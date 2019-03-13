using System;
namespace MarksManagementSystem.Models
{
    public class Marks
    {
        public Marks()
        {
        }
        public int Id { get; set; }
        public int SubId { get; set; }
        public int StuId { get; set; }
        public int PassedId { get; set; }
        public string Grade { get; set; }
        public int GradePoint { get; set; }




    }
}
