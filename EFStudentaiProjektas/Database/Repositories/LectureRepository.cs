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
    public class LectureRepository : ILectureRepository
    {
        private readonly StudentaiContext _context;

        public LectureRepository(StudentaiContext context)
        {
            _context = context;
        }
        //CRUD
        public void Create(Lecture lecture)
        {
            _context.Lectures.Add(lecture);
            _context.SaveChanges();
        }

        public void Update(Lecture lecture)
        {
            _context.Lectures.Update(lecture);
            _context.SaveChanges();
        }

        public void Delete(int lectureId)
        {
            var lecture = _context.Lectures.SingleOrDefault(d => d.LectureId == lectureId);
            if (lecture != null)
            {
                _context.Lectures.Remove(lecture);
                _context.SaveChanges();
            }
        }

        public Lecture? GetLecturesById(int lectureId)
        {
            return _context.Lectures.SingleOrDefault(l => l.LectureId == lectureId);
        }

        public List<Lecture> GetAllLectures()
        {
            return _context.Lectures.Include(l => l.Students).ThenInclude(l => l.Lectures).ToList();
        }

        public void AddLectureToStudent(Student student, Lecture lecture)
        {
            student.Lectures.Add(lecture);
            _context.SaveChanges();
        }
    }
}
