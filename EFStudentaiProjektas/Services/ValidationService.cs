using EFBaigiamasisStudentai.Database;
using EFBaigiamasisStudentai.Database.Entities;
using EFBaigiamasisStudentai.Database.Interfaces;
using EFBaigiamasisStudentai.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EFBaigiamasisStudentai.Services
{
    public class ValidationService : IValidationService
    {
        private readonly StudentaiContext _context;
        private IDepartmentRepository departmentRepository;
        private ILectureRepository lectureRepository;
        private IStudentRepository studentRepository;

        public ValidationService(StudentaiContext context, IDepartmentRepository departmentRepository, ILectureRepository lectureRepository, IStudentRepository studentRepository)
        {
            this._context = context;
            this.departmentRepository = departmentRepository;
            this.lectureRepository = lectureRepository;
            this.studentRepository = studentRepository;
        }

        public string ValidateStudent(Student student)
        {
            string error = string.Empty;
            error = ValidateEmail(student.Email);
            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }
            error = ValidateStudentName(student.FirstName);
            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }
            error = ValidateStudentLastName(student.LastName);
            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }
            error = ValidateStudentNumber(student.StudentNumber.ToString());
            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }
            error = ValidateDepartmentCode(student.DepartmentCode);
            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }
            error = ValidateStudentNumberExists(student.StudentNumber);
            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }

            return error;
        }

        public string ValidateDepartment(Department department)
        {
            string error = string.Empty;
            error = ValidateDepartmentCode(department.DepartmentCode);
            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }
            error = ValidateDepartmentName(department.DepartmentName);
            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }

            return error;
        }

        public string ValidateLecture(Lecture lecture)
        {
            string error = string.Empty;
            error = ValidateLectureName(lecture.LectureName);
            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }
            error = ValidateLectureDay(lecture.LectureDay);
            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }
            error = ValidateLectureFromToTime(lecture.LectureTimeFrom.ToString(), lecture.LectureTimeTo.ToString());
            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }

            return error;
        }

        public string ValidateAddLectureToDepartment(string departmentCode, int lectureId)
        {
            string error = string.Empty;
            var departmentDb = departmentRepository.GetDepartmentByCode(departmentCode);
            if (departmentDb == null)
            {
                return "Toks departmentas neegzistuoja";
            }
            var lectureDb = lectureRepository.GetLecturesById(lectureId);
            if (lectureDb == null)
            {
                return "Tokia paskaita neegzistuoja";
            }
            var lectureExists = departmentDb.Lectures.SingleOrDefault(l => l.LectureId == lectureId);
            if (lectureExists != null)
            {
                return "Tokia paskaita departamente jau yra";
            }

            return string.Empty;
        }

        public string ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return "El. paštas yra privalomas";
            }
            else
            {
                var emailAttribute = new EmailAddressAttribute();
                if (!emailAttribute.IsValid(email))
                {
                    return "Netinkamas el pašto formatas";
                }
            }
            var parts = email.Split('@');
            // Check if the domain part contains a dot
            var domainParts = parts[1].Split('.');
            if (domainParts.Length < 2)
                return "Netinkamas el pašto formatas";

            if (string.IsNullOrWhiteSpace(email))
            {
                return "El. paštas yra privalomas";
            }
            var student = studentRepository.GetStudentByEmail(email);
            if (student != null)
            {
                return "Pažeistas studento unikalumas, toks el. paštas jau egzistuoja";
            }
            return string.Empty;
        }

        public string ValidateStudentName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                // Check if the length is between 2 and 50
                if (name.Length < 2)
                {
                    return "Studento vardas turi būti ne trumpesnis kaip 2 simboliai";
                }
                if (name.Length > 50)
                {
                    return "Studento vardas turi būti ne ilgesnis kaip 50 simbolių";
                }

                // Check if all characters are letters
                foreach (char c in name)
                {
                    if (!char.IsLetter(c))
                    {
                        return "Vardas turi būti sudarytas tik iš raidžių";
                    }
                }
            }
            else
            {
                return "Studento vardas privalo būti nurodytas";
            }
            return string.Empty;
        }

        public string ValidateStudentLastName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                // Check if the length is between 2 and 50
                if (name.Length < 2)
                {
                    return "Studento pavardė turi būti ne trumpesnis kaip 2 simboliai";
                }
                if (name.Length > 50)
                {
                    return "Studento pavardė turi būti ne ilgesnis kaip 50 simbolių";
                }

                // Check if all characters are letters
                foreach (char c in name)
                {
                    if (!char.IsLetter(c))
                    {
                        return "Pavardė turi būti sudarytas tik iš raidžių";
                    }
                }
            }
            else
            {
                return "Studento vardas privalo būti nurodytas";
            }
            return string.Empty;
        }

        public string ValidateOnlyNumbers(string input)
        {
            if (!int.TryParse(input, out int number))
            {
                return "Galima įvesti tik skaičius";
            }
            return string.Empty;
        }

        public string ValidateStudentNumber(string number)
        {
            if (!int.TryParse(number, out int studentNumber))
            {
                return "Numeris turi būti sudarytas tik iš skaičių";
            }

            if (studentNumber > 99999999 || studentNumber < 10000000)
            {
                return "Studento numeris turi būti tikslaiai 8 simbolių ilgio";
            }
            return string.Empty;
        }

        public string ValidateStudentNumberExists(int studentNumber)
        {
            var student = studentRepository.GetStudentByNumber(studentNumber);
            if (student != null)
            {
                return "Studentas su tokiu numeriu jau egzistuoja";
            }

            return string.Empty;
        }

        public string ValidateStudentDepartmentCode(string departmentCode)
        {
            if (string.IsNullOrWhiteSpace(departmentCode))
            {
                return "Departamento kodas yra privalomas";
            }

            if (departmentCode.Length != 6)
            {
                return "Departamento kodas turi būti 6 simbolių ilgio";
            }
            var department = departmentRepository.GetDepartmentByCode(departmentCode);
            if (department == null)
            {
                return "Toks departmento kodas neegzistuoja";
            }

            return string.Empty;
        }

        public string ValidateDepartmentCode(string departmentCode)
        {
            if (string.IsNullOrWhiteSpace(departmentCode))
            {
                return "Departamento kodas yra privalomas";
            }
            foreach (char c in departmentCode)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    return "Departamento kodas gali būti tik raidės ir skaičiai";
                }
            }
            if (departmentCode.Length != 6)
            {
                return "Departamento kodas turi būti 6 simbolių ilgio";
            }
            var department = departmentRepository.GetDepartmentByCode(departmentCode);
            if (department != null)
            {
                return "Toks departmento kodas jau egzistuoja";
            }

            return string.Empty;
        }

        public string ValidateDepartmentName(string departmentName)
        {
            if (string.IsNullOrWhiteSpace(departmentName))
            {
                return "Departamento pavadinimas yra privalomas";
            }
            foreach (char c in departmentName)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    return "Departamento pavadinimas gali būti tik raidės ir skaičiai";
                }
            }
            if (departmentName.Length < 3)
            {
                return "Departamento pavadinimas turi būti ne trumpesnis kaip 3 simboliai";
            }
            return string.Empty;
        }

        public string ValidateLectureName(string lectureName)
        {
            if (string.IsNullOrWhiteSpace(lectureName))
            {
                return "Paskaitos pavadinimas yra privalomas";
            }
            if (lectureName.Length < 5)
            {
                return "Paskaitos pavadinimas turi būti ne trumpesnis kaip 5 simboliai";
            }
            return string.Empty;
        }

        public string ValidateLectureFromToTime(string startTime, string endTime)
        {
            if (!TimeOnly.TryParse(startTime, out TimeOnly start) || !TimeOnly.TryParse(endTime, out TimeOnly end))
            {
                return "Neteisingas laiko formatas";
            }

            if (start.Hour >= 24 || end.Hour >= 24)
            {
                return "Pradžios ir pabaigos laikas turi būti tarp 00:00 ir 24:00";
            }

            if (end < start)
            {
                return "Pabaigos laikas negali būti ankstesnis už pradžios laiką";
            }
            return string.Empty;
        }

        public string ValidateTimeOnly(string time)
        {
            if (!TimeOnly.TryParse(time, out TimeOnly timeOnly))
            {
                return "Neteisingas laiko formatas";
            }
            return string.Empty;
        }

        public string ValidateLectureDay(string? lectureDay)
        {
            if (string.IsNullOrWhiteSpace(lectureDay))
            {
                return string.Empty;
            }

            string[] validDays = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };

            if (lectureDay != null && !validDays.Contains(lectureDay))
            {
                return "Savaitės diena gali būti tik Monday, Tuesday, Wednesday, Thursday, Friday";
            }
            return string.Empty;
        }

        public string ValidateAddStudentDepartment(string departmentCode, int studentNumber)
        {
            var departmentExists = departmentRepository.GetDepartmentByCode(departmentCode);
            if (departmentExists == null)
            {
                return "Depapartment code does not exist";
            }
            var studentDb = studentRepository.GetStudentByNumber(studentNumber);
            if (studentDb == null)
            {
                return "Toks studentas neegzistuoja";
            }
            return string.Empty;
        }

        public string ValidateAddLectureToStudent(int studentNumber, int lectureId)
        {
            string error = string.Empty;
            var studentDb = studentRepository.GetStudentByNumber(studentNumber);
            if (studentDb == null)
            {
                return "Toks studentas neegzistuoja";
            }
            var lectureDb = lectureRepository.GetLecturesById(lectureId);
            if (lectureDb == null)
            {
                return "Tokia paskaita neegzistuoja";
            }
            var departmentDb = departmentRepository.GetDepartmentByCode(studentDb.DepartmentCode).Lectures.SingleOrDefault(l => l.LectureId == lectureId);
            if (departmentDb == null)
            {
                return "Paskaita nepriklauso departmentui";
            }



            return string.Empty;
        }

        //validaciju metodai
    }
}
