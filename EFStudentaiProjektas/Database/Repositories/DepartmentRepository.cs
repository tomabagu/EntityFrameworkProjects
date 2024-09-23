using EFBaigiamasisStudentai.Database.Entities;
using EFBaigiamasisStudentai.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFBaigiamasisStudentai.Database.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly StudentaiContext _context;

        public DepartmentRepository(StudentaiContext context)
        {
            _context = context;
        }

        //CRUD
        public void Create(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
        }

        public void Update(Department department)
        {
            _context.Departments.Update(department);
            _context.SaveChanges();
        }

        public void Delete(string departmentCode)
        {
            var department = _context.Departments.SingleOrDefault(d => d.DepartmentCode.Equals(departmentCode));
            if (department != null)
            {
                _context.Departments.Remove(department);
                _context.SaveChanges();
            }
        }

        public Department? GetDepartmentByCode(string departmentCode)
        {
            return _context.Departments.Include(d => d.Lectures).SingleOrDefault(d => d.DepartmentCode.Equals(departmentCode));
        }

        public List<Department> GetAllDepartments()
        {
            return _context.Departments.Include(d => d.Students).ThenInclude(d => d.Lectures).ToList();
        }

        public void AddLectureToDepartment(Department department, Lecture lecture)
        {
            department.Lectures.Add(lecture);
            _context.SaveChanges();
        }

        public void TransferStudentToDepartment (Student student, Department department)
        {
            student.Lectures.Clear();
            student.DepartmentCode=department.DepartmentCode;
            _context.SaveChanges();
        }
    }
}
