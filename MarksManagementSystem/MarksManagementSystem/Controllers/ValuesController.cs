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
        public List<Student> Get()
        {
            List<Student> studentList = new List<Student>();
            // var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            // optionsBuilder.UseMySQL("server=localhost;port=3306;userid=root;pwd=Rajeev_97;database=testDb;sslmode=none;");
            using (DataContext dbcontext = context)
            {
                studentList = (from s in dbcontext.Students
                               where s.Id == 1
                               select s).ToList<Student>();
            }

            //bool isAuthenticated = User.Identity.IsAuthenticated;
            return studentList;
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