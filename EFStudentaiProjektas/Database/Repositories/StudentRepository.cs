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
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentaiContext _context;

        public StudentRepository(StudentaiContext context)
        {
            _context = context;
        }

        public void Create(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public void Update(Student student)
        {
            _context.Entry(student).CurrentValues.SetValues(student);
            _context.SaveChanges();
        }

        public void Delete(int studentNumber)
        {
            var student = _context.Students.SingleOrDefault(d => d.StudentNumber == studentNumber);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }

        public Student? GetStudentByNumber(int studentNumber)
        {
            return _context.Students.SingleOrDefault(l => l.StudentNumber == studentNumber);
        }

        public List<Student> GetAllStudents()
        {
            return _context.Students.Include(l => l.Lectures).ToList();
        }

        public void DeleteStudentsLectures(Student student)
        {
            student.Lectures.Clear();
            _context.SaveChanges();
        }

        public Student? GetStudentByEmail(string email)
        {
            return _context.Students.SingleOrDefault(l => l.Email.Equals(email));
        }


    }
}
