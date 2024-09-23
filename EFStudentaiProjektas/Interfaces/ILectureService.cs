using EFBaigiamasisStudentai.Database.Entities;

namespace EFBaigiamasisStudentai.Interfaces
{
    public interface ILectureService
    {
        string AddLectureToStudent(Student student, Lecture lecture);
        string CreateLecture(Lecture lecture);
        List<Lecture> GetAllLectures();
        Lecture GetLectureById(int lectureId);
    }
}