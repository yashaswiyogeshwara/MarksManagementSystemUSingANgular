using System;
namespace MarksManagementSystem.Models
{
    public class ClassInfo
    {
        public ClassInfo()
        {
        }
        /*
         params.set('yearOfJoining', yearOfJoining['YearOfJoining']);
    params.set('department', department['Department']);
    params.set('year', year['Year']);
    params.set('semester', semester['Semester']);
    params.set('section', section['Section']);
        */
        public string YearOfJoining { get; set; }
        public string Department { get; set; }
        public string Year { get; set; }
        public string Semester { get; set; }
        public string Section { get; set; }


    }
}
