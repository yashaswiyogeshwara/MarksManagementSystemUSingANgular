﻿using System;
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
        public StudentProfile Get(string hallTicketNo)
        {
            StudentProfile studentProfile = new StudentProfile();
            // var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            // optionsBuilder.UseMySQL("server=localhost;port=3306;userid=root;pwd=Rajeev_97;database=testDb;sslmode=none;");
            using (DataContext dbcontext = context)
            {
                List<StudentMarks> studentList = (from stu in dbcontext.Students join m in dbcontext.Marks 
                               on stu.Id equals m.StudentId 
                               join sub in dbcontext.Subject on m.SubjectId equals sub.Id
                               join std in dbcontext.Standard on m.StandardId equals std.Id
                               where stu.Hallticket == hallTicketNo
                               select new StudentMarks{
                                   Hallticket = stu.Hallticket,
                                   SubjectName = sub.Name,
                                   SubjectCode = sub.Code,
                                   Grade = m.Grade,
                                   GradePoint = m.GradePoint,
                                   Year = std.Year,
                                   Sem = std.Sem
                               }).ToList<StudentMarks>();

                List<Standard> standards = (from std in dbcontext.Standard
                                            select std).ToList<Standard>();
                for(int i=0; i<standards.Count; i++) {
                   List<StudentMarks> marksRecords = studentList.FindAll(x => x.Year == standards[i].Year && x.Sem == standards[i].Sem);
                    if (marksRecords.Count > 0) {
                        int avg = marksRecords.Sum(x => x.GradePoint) / marksRecords.Count;
                        StudentMarksByYear recordsByYear = new StudentMarksByYear();
                        recordsByYear.StudentMarks = marksRecords;
                        recordsByYear.Average = avg;
                        recordsByYear.NoOfBacklogs = marksRecords.Count(x => x.Grade == "F");
                        studentProfile.StudentMarksByYearList.Add(recordsByYear);
                    }
                }

            }

            //bool isAuthenticated = User.Identity.IsAuthenticated;
            return studentProfile;
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