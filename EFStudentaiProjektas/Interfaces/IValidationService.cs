using EFBaigiamasisStudentai.Database.Entities;

namespace EFBaigiamasisStudentai.Interfaces
{
    public interface IValidationService
    {
        string ValidateAddLectureToDepartment(string departmentCode, int lectureId);
        string ValidateAddLectureToStudent(int studentNumber, int lectureId);
        string ValidateAddStudentDepartment(string departmentCode, int studentNumber);
        string ValidateDepartment(Department department);
        string ValidateDepartmentCode(string departmentCode);
        string ValidateDepartmentName(string departmentName);
        string ValidateEmail(string email);
        string ValidateLecture(Lecture lecture);
        string ValidateLectureDay(string? lectureDay);
        string ValidateLectureFromToTime(string startTime, string endTime);
        string ValidateLectureName(string lectureName);
        string ValidateOnlyNumbers(string input);
        string ValidateStudent(Student student);
        string ValidateStudentDepartmentCode(string departmentCode);
        string ValidateStudentLastName(string name);
        string ValidateStudentName(string name);
        string ValidateStudentNumber(string number);
        string ValidateStudentNumberExists(int studentNumber);
        string ValidateTimeOnly(string time);
    }
}