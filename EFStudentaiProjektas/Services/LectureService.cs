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
    public class LectureService : ILectureService
    {
        private ILectureRepository lectureRepository;
        private IValidationService validationService;

        public LectureService(ILectureRepository lectureRepository, IValidationService validationService)
        {
            this.lectureRepository = lectureRepository;
            this.validationService = validationService;
        }

        public string CreateLecture(Lecture lecture)
        {
            string error = string.Empty;
            error = validationService.ValidateLecture(lecture);
            if (!error.IsNullOrEmpty())
            {
                return error;
            }
            if (string.IsNullOrWhiteSpace(lecture.LectureDay))
            {
                lecture.LectureDay = null;
            }
            lectureRepository.Create(lecture);
            return error;
        }

        public List<Lecture> GetAllLectures()
        {
            return lectureRepository.GetAllLectures();
        }

        public Lecture GetLectureById(int lectureId)
        {
            return lectureRepository.GetLecturesById(lectureId);
        }

        public string AddLectureToStudent(Student student, Lecture lecture)
        {
            string error = string.Empty;
            error = validationService.ValidateAddLectureToStudent(student.StudentNumber, lecture.LectureId.GetValueOrDefault());
            if (!error.IsNullOrEmpty())
            {
                return error;
            }
            lectureRepository.AddLectureToStudent(student, lecture);
            return string.Empty;
        }
    }
}
