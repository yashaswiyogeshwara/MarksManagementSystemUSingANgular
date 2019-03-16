using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarksManagementSystem.Models
{
    public class StudentProfile {
        public List<StudentMarksByYear> StudentMarksByYearList { get; set; }
        public int NAAC { get; set; }

    } 
    public class StudentMarksByYear
    {
        public List<StudentMarks> StudentMarks { get; set; }
        public double Average { get; set; }
        public int NoOfBacklogs { get; set; }
    }
}
