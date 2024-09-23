using EFBaigiamasisStudentai.Database.Entities;

namespace EFBaigiamasisStudentai.Interfaces
{
    public interface IDepartmentService
    {
        string AddLectureToDepartment(Department department, Lecture lecture);
        string CreateDepartment(Department department);
        List<Department> GetAllDepartments();
        Department? GetDepartmentByCode(string departmentCode);
        void TransferStudentToDepartment(Department department, Student student);
    }
}