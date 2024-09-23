using EFBaigiamasisStudentai.Database.Entities;

namespace EFBaigiamasisStudentai.Interfaces
{
    public interface IStudentService
    {
        string AddStudentToDepartment(string departmentCode, Student student);
        string CreateStudent(Student student);
        List<Student> GetAllStudents();
        Student? GetStudentByEmail(string email);
        Student? GetStudentByNumber(int studentNumber);
    }
}