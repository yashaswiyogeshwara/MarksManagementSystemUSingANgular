using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarksManagementSystem.Models
{
    public class Student
    {
        public Student()
        {
        }
        public int Id { get; set; }
        public string Hallticket { get; set; }
        public int Yearofjoin { get; set; }
        public string Dept { get; set; }
        public string Section { get; set; }
        public int Backlogs { get; set; }
        public int NAAC { get; set; }
    }
}
