using System;
namespace MarksManagementSystem.Models
{
    public class Marks
    {
        public Marks()
        {
        }
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int StudentId { get; set; }
        public int StandardId { get; set; }
        public string Grade { get; set; }
        public int GradePoint { get; set; }
    }
}
