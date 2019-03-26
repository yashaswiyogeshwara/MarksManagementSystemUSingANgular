using MarksManagementSystem.DAL;
using MarksManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarksManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : Controller
    {
        public readonly UserContext UserContext;
        public readonly DataContext context;

        public ValuesController(DataContext dataContext, UserContext sqlCOntext)
        {
            this.context = dataContext;
            this.UserContext = sqlCOntext;
            // var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        }
        // GET api/values
        [HttpGet("GetStudents")]
        public ActionResult GetStudents(string hallTicketNo)
        {
            bool accessNotAllowed =
                String.Equals(Request.Headers.First(x => x.Key == "Authorization").Value, "unauth");
            if (accessNotAllowed)
            {
                return Ok(new { success = false, mess = "Not authenticated" });
            }
            StudentProfile studentProfile = new StudentProfile();
            studentProfile.StudentMarksByYearList = new List<StudentMarksByYear>();
            try
            {
                using (DataContext dbcontext = context)
                {
                    List<StudentMarks> studentList = (from stu in dbcontext.Students
                                                      join m in dbcontext.Marks
                                                      on stu.Id equals m.StudentId
                                                      join sub in dbcontext.Subject on m.SubjectId equals sub.Id
                                                      join std in dbcontext.Standard on m.StandardId equals std.Id
                                                      where stu.Hallticket == hallTicketNo
                                                      select new StudentMarks
                                                      {
                                                          Hallticket = stu.Hallticket,
                                                          SubjectName = sub.Name,
                                                          SubjectCode = sub.Code,
                                                          Grade = m.Grade,
                                                          GradePoint = m.GradePoint,
                                                          Year = std.Year,
                                                          Sem = std.Sem
                                                      }).ToList<StudentMarks>();
                    if (studentList.Count() > 0)
                    {
                        studentProfile.TotalAverage = studentList.Average((x) => x.GradePoint);
                        studentProfile.TotalNoOfBacklogs = studentList.Count((x) => x.Grade == "F");
                    }

                    List<Standard> standards = (from std in dbcontext.Standard
                                                select std).ToList<Standard>();
                    for (int i = 0; i < standards.Count; i++)
                    {
                        List<StudentMarks> marksRecords = studentList.FindAll(x => x.Year == standards[i].Year && x.Sem == standards[i].Sem);
                        if (marksRecords.Count > 0)
                        {
                            double avg = marksRecords.Average(x => x.GradePoint);
                            StudentMarksByYear recordsByYear = new StudentMarksByYear();
                            recordsByYear.StudentMarks = marksRecords;
                            recordsByYear.Average = avg;
                            recordsByYear.NoOfBacklogs = marksRecords.Count(x => x.Grade == "F");
                            studentProfile.StudentMarksByYearList.Add(recordsByYear);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                return Ok(new { mess = ex.Message });
            }

            return Ok(new { data = studentProfile });
        }



        // GET api/values
        [HttpGet("GetClass")]
        public ActionResult GetClass(string YearOfJoining, string Department, string Year, string Semester, string Section)
        {
            bool accessNotAllowed =
                String.Equals(Request.Headers.First(x => x.Key == "Authorization").Value, "unauth");
            if (accessNotAllowed)
            {
                return Ok(new { success = false, mess = "Not authenticated" });
            }

            List<ClassProfile> classProfile = new List<ClassProfile>();

            int yoj;
            int year;
            int sem;
            if (int.TryParse(YearOfJoining, out yoj) &&
            int.TryParse(Year, out year) && int.TryParse(Semester, out sem))
            {
                try
                {
                    using (DataContext dbcontext = context)
                    {
                        //studentMiniDataList = (from stu in dbcontext.Students
                        //                       join m in dbcontext.Marks
                        //                       on stu.Id equals m.StudentId
                        //                       join sub in dbcontext.Subject on m.SubjectId equals sub.Id
                        //                       join std in dbcontext.Standard on m.StandardId equals std.Id
                        //                       where stu.Yearofjoin == yoj && std.Year == year && stu.Dept == Department && std.Sem == sem && stu.Section == Section
                        //                       group new { m, stu, std, sub } by stu.Hallticket into joined
                        //                       select new StudentMiniData
                        //                       {
                        //                           HallTicket = joined.Key.ToString(),
                        //                           Average = joined.Average(x => x.m.GradePoint),
                        //                           NoOfBacklogs = joined.Count(x => x.m.Grade == "F"),
                        //                           NAAC = joined.Min(x => x.stu.NAAC)

                        //                       }).ToList<StudentMiniData>();
                        List<SubjectDataPerSem> studentMarksList = (from stu in dbcontext.Students
                                               join m in dbcontext.Marks
                                               on stu.Id equals m.StudentId
                                               join sub in dbcontext.Subject on m.SubjectId equals sub.Id
                                               join std in dbcontext.Standard on m.StandardId equals std.Id
                                               where stu.Yearofjoin == yoj && std.Year == year && stu.Dept == Department && std.Sem == sem && stu.Section == Section
                                               orderby stu.Hallticket, sub.Name
                                               select new SubjectDataPerSem
                                               {
                                                   HallTicket = stu.Hallticket,
                                                   GradeInSubject = m.Grade,
                                                   GradePointInSubject = m.GradePoint,
                                                   NAAC = stu.NAAC,
                                                   SubjectName = sub.Name
                                                   
                                                  
                                               }).ToList<SubjectDataPerSem>();

                        studentMarksList.ForEach(x =>
                        {
                            ClassProfile prof = classProfile.FirstOrDefault((y)=> y.HallTicket == x.HallTicket);
                            if (prof != null)
                            {
                                prof.StudentMarks.Add(new StudentMarks
                                {
                                    Hallticket = x.HallTicket,
                                    SubjectName = x.SubjectName,
                                    Grade = x.GradeInSubject,
                                    GradePoint = x.GradePointInSubject
                                });
                            }
                            else {
                                ClassProfile cp = new ClassProfile()
                                {
                                    HallTicket = x.HallTicket,
                                    StudentMarks = new List<StudentMarks>()
                                };
                                cp.StudentMarks.Add(new StudentMarks {

                                    Hallticket = x.HallTicket,
                                    SubjectName = x.SubjectName,
                                    Grade = x.GradeInSubject,
                                    GradePoint = x.GradePointInSubject
                                });
                                classProfile.Add(cp);
                            }
                        });

                        classProfile.ForEach(x =>
                        {
                            x.Average = x.StudentMarks.Average((z) => z.GradePoint );
                            x.NAAC = x.NAAC;
                        });
                    }
                }
                catch (Exception ex)
                {
                    return Ok(new { mess = ex.Message });
                }
            }

            return Ok(new { data = classProfile });
        }


        // GET api/values
        [HttpGet("GetSubject")]
        public ActionResult GetSubject(string YearOfJoining, string Department, string Year, string Semester, string Section, string SubjectName)
        {
            SubjectData subjectData = new SubjectData();
            //  List<SubjectDataPerSem> subjectDataPerSem = new List<SubjectDataPerSem>();
            int yoj;
            int year;
            int sem;
            if (int.TryParse(YearOfJoining, out yoj) &&
            int.TryParse(Year, out year) && int.TryParse(Semester, out sem))
            {
                try
                {
                    using (DataContext dbcontext = context)
                    {
                        List<SubjectDataPerSem> subjectDataPerSem = (from stu in dbcontext.Students
                                                                     join m in dbcontext.Marks
                                                                     on stu.Id equals m.StudentId
                                                                     join sub in dbcontext.Subject on m.SubjectId equals sub.Id
                                                                     join std in dbcontext.Standard on m.StandardId equals std.Id
                                                                     where stu.Yearofjoin == yoj && std.Year == year && stu.Dept == Department && std.Sem == sem && stu.Section == Section && sub.Name == SubjectName
                                                                     group new { m, stu, std, sub } by stu.Hallticket into joined
                                                                     select new SubjectDataPerSem
                                                                     {
                                                                         HallTicket = joined.Min(x => x.stu.Hallticket),
                                                                         GradeInSubject = joined.Min(x => x.m.Grade),
                                                                         GradePointInSubject = joined.Min(x => x.m.GradePoint),

                                                                     }).ToList<SubjectDataPerSem>();

                        if (subjectDataPerSem.Count() > 0)
                        {
                            double TotalAverage = subjectDataPerSem.Average((x) => x.GradePointInSubject);
                            subjectData.TotalAverage = TotalAverage;
                            subjectData.subjectDataPerSem = subjectDataPerSem;
                        }
                    }

                }
                catch (Exception ex)
                {
                    return Ok(new { mess = ex.Message });
                }
            }

            return Ok(new { data = subjectData });
        }


        [HttpGet("GetSubjectBacklogs")]
        public ActionResult GetSubjectBacklogs(string YearOfJoining, string Department, string Year, string Semester, string Section, string SubjectName)
        {
            SubjectData subjectData = new SubjectData();
            //  List<SubjectDataPerSem> subjectDataPerSem = new List<SubjectDataPerSem>();
            int yoj;
            int year;
            int sem;
            if (int.TryParse(YearOfJoining, out yoj) &&
            int.TryParse(Year, out year) && int.TryParse(Semester, out sem))
            {
                try
                {
                    using (DataContext dbcontext = context)
                    {
                        List<SubjectDataPerSem> subjectDataPerSem = (from stu in dbcontext.Students
                                                                     join m in dbcontext.Marks
                                                                     on stu.Id equals m.StudentId
                                                                     join sub in dbcontext.Subject on m.SubjectId equals sub.Id
                                                                     join std in dbcontext.Standard on m.StandardId equals std.Id
                                                                     where stu.Yearofjoin == yoj && std.Year == year && stu.Dept == Department && std.Sem == sem && stu.Section == Section && sub.Name == SubjectName && m.Grade.ToLower() =="f"
                                                                     group new { m, stu, std, sub } by stu.Hallticket into joined
                                                                     select new SubjectDataPerSem
                                                                     {
                                                                         HallTicket = joined.Min(x => x.stu.Hallticket),
                                                                         GradeInSubject = joined.Min(x => x.m.Grade),
                                                                         GradePointInSubject = joined.Min(x => x.m.GradePoint),

                                                                     }).ToList<SubjectDataPerSem>();

                        if (subjectDataPerSem.Count() > 0)
                        {
                            double TotalAverage = subjectDataPerSem.Average((x) => x.GradePointInSubject);
                            subjectData.TotalAverage = TotalAverage;
                            subjectData.subjectDataPerSem = subjectDataPerSem;
                        }
                    }

                }
                catch (Exception ex)
                {
                    return Ok(new { mess = ex.Message });
                }
            }

            return Ok(new { data = subjectData });
        }


        [HttpGet("GetDepartment")]
        public ActionResult GetDepartment(string YearOfJoining, string Department, string Year, string Semester)
        {
            bool accessNotAllowed =
                String.Equals(Request.Headers.First(x => x.Key == "Authorization").Value, "unauth");
            if (accessNotAllowed)
            {
                return Ok(new { success = false, mess = "Not authenticated" });
            }
            List<DepartmentData> departmentDataList = new List<DepartmentData>();
            int yoj;
            int year;
            int sem;
            if (int.TryParse(YearOfJoining, out yoj) &&
            int.TryParse(Year, out year) && int.TryParse(Semester, out sem))
            {
                try
                {
                    using (DataContext dbcontext = context)
                    {

                        departmentDataList = (from stu in dbcontext.Students
                                              join m in dbcontext.Marks
                                              on stu.Id equals m.StudentId
                                              join sub in dbcontext.Subject on m.SubjectId equals sub.Id
                                              join std in dbcontext.Standard on m.StandardId equals std.Id
                                              where stu.Yearofjoin == yoj && std.Year == year && stu.Dept == Department && std.Sem == sem
                                              group new { m, stu, std, sub } by stu.Section into joined
                                              select new DepartmentData
                                              {
                                                  Section = int.Parse(joined.Key),
                                                  Average = joined.Average(x => x.m.GradePoint),


                                              }).ToList<DepartmentData>();

                    }

                }
                catch (Exception ex)
                {
                    return Ok(new { mess = ex.Message });
                }
            }

            return Ok(new { data = departmentDataList });
        }

        [HttpGet("AddSubject")]
        public ActionResult AddSubject(string Department, string Year, string Semester, string SubjectName, string SubjectCode)
        {
            int year;
            int sem;
            if (int.TryParse(Year, out year) && int.TryParse(Semester, out sem))
            {
                try
                {
                    using (DataContext _context = context)
                    {
                        Standard _std = (from s in _context.Standard
                                         where s.Year == year && s.Sem == sem
                                         select s).FirstOrDefault();
                        if (_std != null)
                        {
                            int stdId = _std.Id;
                            _context.Subject.Add(new Subject()
                            {
                                Name = SubjectName,
                                Code = SubjectCode,
                                StandardId = stdId
                            });
                            _context.SaveChanges();
                            return Ok(new { success = true, mess = "Successfully updated data" });
                        }
                        else {
                            return Ok(new { success = false, mess = "Year and Sem info is not present. Please update appropriately" });
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Ok(new { success= false , mess = ex.Message });
                }
                return null;
            }
            return null;
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}