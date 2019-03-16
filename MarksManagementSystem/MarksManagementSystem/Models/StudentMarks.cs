using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarksManagementSystem.Models
{
    public class StudentMarks
    {
        /*
         stu.Hallticket,
                                   sub.Name,
                                   sub.Code,
                                   m.Grade,
                                   m.GradePoint,
                                   std.Year,
                                   std.Sem
             */
        public string Hallticket { get; set; }
        public string SubjectName { get; set; }
        public string SubjectCode { get; set; }
        public int Year { get; set; }
        public int Sem { get; set; }
        public string Grade { get; set; }
        public int GradePoint { get; set; }

    }
}
