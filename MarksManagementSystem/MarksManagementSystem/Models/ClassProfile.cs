using System;
using System.Collections.Generic;

namespace MarksManagementSystem.Models
{
    public class ClassProfile
    {
        public ClassProfile()
        {
        }
        public string HallTicket { get; set; }
        public double Average { get; set; }
        public int NoOfBacklogs { get; set; }
        public int NAAC { get; set; }

        public List<StudentMarks> StudentMarks { get; set; }
    }
}
