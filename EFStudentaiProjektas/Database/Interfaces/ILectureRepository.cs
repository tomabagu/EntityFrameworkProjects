using EFBaigiamasisStudentai.Database.Entities;

namespace EFBaigiamasisStudentai.Database.Interfaces
{
    public interface ILectureRepository
    {
        void AddLectureToStudent(Student student, Lecture lecture);
        void Create(Lecture lecture);
        void Delete(int lectureId);
        List<Lecture> GetAllLectures();
        Lecture? GetLecturesById(int lectureId);
        void Update(Lecture lecture);
    }
}