using EFBaigiamasisStudentai.Database.Entities;

namespace EFBaigiamasisStudentai.Database.Interfaces
{
    public interface IStudentRepository
    {
        void Create(Student student);
        void Delete(int studentNumber);
        void DeleteStudentsLectures(Student student);
        List<Student> GetAllStudents();
        Student? GetStudentByEmail(string email);
        Student? GetStudentByNumber(int studentNumber);
        void Update(Student student);
    }
}