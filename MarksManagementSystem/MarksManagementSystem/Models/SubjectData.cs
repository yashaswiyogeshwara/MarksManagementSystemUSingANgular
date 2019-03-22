using System;
using System.Collections.Generic;

namespace MarksManagementSystem.Models
{
    public class SubjectData
    {
        public SubjectData()
        {
        }
        public List<SubjectDataPerSem> subjectDataPerSem { get; set; }
        public double TotalAverage { get; set; }
    }
    public class SubjectDataPerSem
    {

        public string HallTicket { get; set; }
        public string GradeInSubject { get; set; }
        public int GradePointInSubject { get; set; }
    }

}
