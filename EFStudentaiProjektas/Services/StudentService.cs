using EFBaigiamasisStudentai.Database.Entities;
using EFBaigiamasisStudentai.Database.Interfaces;
using EFBaigiamasisStudentai.Database.Repositories;
using EFBaigiamasisStudentai.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFBaigiamasisStudentai.Services
{
    public class StudentService : IStudentService
    {
        private IStudentRepository studentRepository;
        private IValidationService validationService;

        public StudentService(IStudentRepository studentRepository, IValidationService validationService)
        {
            this.studentRepository = studentRepository;
            this.validationService = validationService;
        }

        public string CreateStudent(Student student)
        {
            string error = string.Empty;
            error = validationService.ValidateStudent(student);
            if (!error.IsNullOrEmpty())
            {
                return error;
            }
            studentRepository.Create(student);
            return error;
        }

        public List<Student> GetAllStudents()
        {
            return studentRepository.GetAllStudents();
        }

        public Student? GetStudentByNumber(int studentNumber)
        {
            return studentRepository.GetStudentByNumber(studentNumber);
        }

        public string AddStudentToDepartment(string departmentCode, Student student)
        {
            var error = string.Empty;
            error = validationService.ValidateAddStudentDepartment(departmentCode, student.StudentNumber);
            if (!error.IsNullOrEmpty())
            {
                return error;
            }
            student.DepartmentCode = departmentCode;
            studentRepository.Update(student);
            return string.Empty;
        }

        public Student? GetStudentByEmail(string email)
        {
            return studentRepository.GetStudentByEmail(email);
        }
    }
}
