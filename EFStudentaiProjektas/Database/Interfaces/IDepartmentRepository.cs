using EFBaigiamasisStudentai.Database.Entities;

namespace EFBaigiamasisStudentai.Database.Interfaces
{
    public interface IDepartmentRepository
    {
        void AddLectureToDepartment(Department department, Lecture lecture);
        void Create(Department department);
        void Delete(string departmentCode);
        List<Department> GetAllDepartments();
        Department? GetDepartmentByCode(string departmentCode);
        void TransferStudentToDepartment(Student student, Department department);
        void Update(Department department);
    }
}