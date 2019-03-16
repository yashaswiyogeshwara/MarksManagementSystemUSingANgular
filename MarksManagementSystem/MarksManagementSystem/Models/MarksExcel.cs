using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarksManagementSystem.Models
{
    public class MarksExcel
    {
        //Hallticket No	Subject Code	Subject Name	Grade	Grade Points			
        public string Hallticket { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public string Grade { get; set; }
        public int GradePoints { get; set; }
    }
}
