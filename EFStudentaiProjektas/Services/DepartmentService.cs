using EFBaigiamasisStudentai.Database.Entities;
using EFBaigiamasisStudentai.Database.Interfaces;
using EFBaigiamasisStudentai.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFBaigiamasisStudentai.Services
{
    public class DepartmentService : IDepartmentService
    {
        private IDepartmentRepository departmentRepository;
        private IValidationService validationService;

        public DepartmentService(IDepartmentRepository departmentRepository, IValidationService validationService)
        {
            this.departmentRepository = departmentRepository;
            this.validationService = validationService;
        }

        public string CreateDepartment(Department department)
        {
            string error = string.Empty;
            error = validationService.ValidateDepartment(department);
            if (!error.IsNullOrEmpty())
            {
                return error;
            }
            departmentRepository.Create(department);
            return error;

        }

        public List<Department> GetAllDepartments()
        {
            return departmentRepository.GetAllDepartments();
        }

        public Department? GetDepartmentByCode(string departmentCode)
        {
            return departmentRepository.GetDepartmentByCode(departmentCode);
        }

        public string AddLectureToDepartment(Department department, Lecture lecture)
        {
            string error = string.Empty;
            error = validationService.ValidateAddLectureToDepartment(department.DepartmentCode, lecture.LectureId.GetValueOrDefault());
            if (!error.IsNullOrEmpty())
            {
                return error;
            }
            departmentRepository.AddLectureToDepartment(department, lecture);
            return error;
        }

        public void TransferStudentToDepartment(Department department, Student student)
        {
            departmentRepository.TransferStudentToDepartment(student, department);
        }
    }
}
