using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFBaigiamasisStudentai.Database.Entities
{
    public class Student
    {
        public Student(int studentNumber, string firstName, string lastName, string email, string departmentCode)
        {
            StudentNumber = studentNumber;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DepartmentCode = departmentCode;
        }

        public int StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();
        public Department Department { get; set; }
        public string DepartmentCode { get; set; }
    }
}
