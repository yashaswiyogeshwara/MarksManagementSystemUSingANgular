using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarksManagementSystem.DAL;
using MarksManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

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
            StudentProfile studentProfile = new StudentProfile();
            studentProfile.StudentMarksByYearList = new List<StudentMarksByYear>();
            try {
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
            } catch (Exception ex) {
                return Ok(new { mess = ex.Message });
            }
            
            return Ok(new { data = studentProfile});
        }



        // GET api/values
        [HttpGet("GetClass")]
        public ActionResult GetClass(string YearOfJoining ,string Department,string Year,string Semester,string Section)
        {
            List<StudentMiniData> studentMiniDataList = new List<StudentMiniData>();
            int yoj;
            int year;
            int sem;
            int sec;
            if (int.TryParse(YearOfJoining, out yoj) &&
            int.TryParse(Year, out year) && int.TryParse(Semester, out sem) && int.TryParse(Section, out sec)) {
                try
                {
                    using (DataContext dbcontext = context)
                    {
                        studentMiniDataList = (from stu in dbcontext.Students
                                                          join m in dbcontext.Marks
                                                          on stu.Id equals m.StudentId
                                                          join sub in dbcontext.Subject on m.SubjectId equals sub.Id
                                                          join std in dbcontext.Standard on m.StandardId equals std.Id
                                                          where stu.Yearofjoin == yoj && std.Year == year && stu.Dept == Department && std.Sem == sem
                                                          group new {m,stu,std,sub} by stu.Hallticket into joined
                                                          select new StudentMiniData
                                                          { 
                                                              HallTicket = joined.Key.ToString(),
                                                              Average = joined.Average(x=> x.m.GradePoint),
                                                              NoOfBacklogs = joined.Count(x => x.m.Grade == "F"),
                                                              NAAC = joined.Min(x=> x.stu.NAAC)
                                                          
                                                      }).ToList<StudentMiniData>();

                    }
                }
                catch (Exception ex)
                {
                    return Ok(new { mess = ex.Message });
                }
            }

            return Ok(new { data = studentMiniDataList });
        }


        [HttpGet("GetDepartment")]
        public ActionResult GetDepartment(string YearOfJoining, string Department, string Year, string Semester )
        {
            List<DepartmentData> departmentDataList = new List<DepartmentData>();
            int yoj;
            int year;
            int sem;
            if (int.TryParse(YearOfJoining, out yoj) &&
            int.TryParse(Year, out year) && int.TryParse(Semester, out sem) )
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
                                              where stu.Yearofjoin == yoj && std.Year == year && stu.Dept == Department
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