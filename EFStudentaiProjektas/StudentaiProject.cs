using EFBaigiamasisStudentai.Database.Entities;
using EFBaigiamasisStudentai.Interfaces;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EFBaigiamasisStudentai
{
    public class StudentaiProject
    {
        private IDepartmentService departmentService;
        private ILectureService lectureService;
        private IStudentService studentService;
        private IValidationService validationService;

        public StudentaiProject(IDepartmentService departmentService, ILectureService lectureService, IStudentService studentService, IValidationService validationService)
        {
            this.departmentService = departmentService;
            this.lectureService = lectureService;
            this.studentService = studentService;
            this.validationService = validationService;
        }

        public void Run()
        {
            var choice = 0;
            while (true) // pagrindinis meniu
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine();

                    Console.WriteLine("1. Sukurti departamentą");
                    Console.WriteLine("2. Sukurti studentą");
                    Console.WriteLine("3. Sukurti paskaitą");
                    Console.WriteLine("4. Pridėti studentą į departamentą");
                    Console.WriteLine("5. Pridėti paskaitas į departamentą");
                    Console.WriteLine("6. Priskirti paskaitas studentui");


                } while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 6);
                switch (choice)
                {
                    case 1:
                        CreateDepartment();
                        Console.WriteLine("Departamentas sukurtas sėkmingai");
                        Console.ReadKey();
                        break;
                    case 2:
                        CreateStudent();
                        Console.WriteLine("Studentas sukurtas sėkmingai");
                        Console.ReadKey();
                        break;
                    case 3:
                        CreateLecture();
                        Console.WriteLine("Paskaita sukurta sėkmingai");
                        Console.ReadKey();
                        break;
                    case 4:
                        AddStudentToDepartment();
                        Console.WriteLine("Studentas departamentui priskirtas sėkmingai");
                        Console.ReadKey();
                        break;
                    case 5:
                        AddLectureToDepartment();
                        Console.WriteLine("Paskaita departamentui priskirta sėkmingai");
                        Console.ReadKey();
                        break;
                    case 6:
                        AddLectureToStudent();
                        Console.WriteLine("Paskaita studentui priskirta sėkmingai");
                        Console.ReadKey();
                        break;
                    default:
                        break;
                }
            }
        }

        public void CreateDepartment()
        {
            string error = string.Empty;
            string departmentCode;
            string departmentName;
            do
            {
                Console.WriteLine("Įveskite departamento kodą");
                departmentCode = Console.ReadLine();
                error = validationService.ValidateDepartmentCode(departmentCode);
                if (!error.IsNullOrEmpty())
                {
                    Console.WriteLine(error);
                }

            } while (!error.IsNullOrEmpty());

            do
            {
                Console.WriteLine("Įveskite departamento pavadinimą");
                departmentName = Console.ReadLine();
                error = validationService.ValidateDepartmentName(departmentName);
                if (!error.IsNullOrEmpty())
                {
                    Console.WriteLine(error);
                }

            } while (!error.IsNullOrEmpty());

            Department newDepartment = new Department(departmentCode, departmentName);
            departmentService.CreateDepartment(newDepartment);
        }

        public void CreateStudent()
        {
            var error = string.Empty;
            string studentNumber;
            string studentFirstName;
            string studentLastName;
            string departmentCode;
            string studentEmail;
            bool isValid;

            do
            {
                Console.WriteLine("Įveskite studento numerį");
                studentNumber = Console.ReadLine();
                error = validationService.ValidateStudentNumber(studentNumber);
                if (!error.IsNullOrEmpty())
                {
                    Console.WriteLine(error);
                }

            } while (!error.IsNullOrEmpty());

            do
            {
                Console.WriteLine("Įveskite studento vardą");
                studentFirstName = Console.ReadLine();
                error = validationService.ValidateStudentName(studentFirstName);
                if (!error.IsNullOrEmpty())
                {
                    Console.WriteLine(error);
                }

            } while (!error.IsNullOrEmpty());

            do
            {
                Console.WriteLine("Įveskite studento pavardę");
                studentLastName = Console.ReadLine();
                error = validationService.ValidateStudentLastName(studentLastName);
                if (!error.IsNullOrEmpty())
                {
                    Console.WriteLine(error);
                }

            } while (!error.IsNullOrEmpty());

            do
            {
                Console.WriteLine("Įveskite studento el.paštą");
                studentEmail = Console.ReadLine();
                error = validationService.ValidateEmail(studentEmail);
                if (!error.IsNullOrEmpty())
                {
                    Console.WriteLine(error);
                }

            } while (!error.IsNullOrEmpty());

            do
            {
                Console.WriteLine("Įveskite studento departamentą");
                departmentCode = Console.ReadLine();
                error = validationService.ValidateStudentDepartmentCode(departmentCode);
                if (!error.IsNullOrEmpty())
                {
                    Console.WriteLine(error);
                }

            } while (!error.IsNullOrEmpty());

            Student newStudent = new Student(int.Parse(studentNumber), studentFirstName, studentLastName, studentEmail, departmentCode);
            studentService.CreateStudent(newStudent);
        }

        public void CreateLecture()
        {
            var error = string.Empty;
            string lectureName;
            string lectureTimeFromHours;
            string lectureTimeToHours;
            string lectureDay;


            do
            {
                Console.WriteLine("Įveskite paskaitos pavadinimą");
                lectureName = Console.ReadLine();
                error = validationService.ValidateLectureName(lectureName);
                if (!error.IsNullOrEmpty())
                {
                    Console.WriteLine(error);
                }

            } while (!error.IsNullOrEmpty());
            do
            {
                do
                {
                    Console.WriteLine("Įveskite paskaitos pradžios laiką HH:MM");
                    lectureTimeFromHours = Console.ReadLine();
                    error = validationService.ValidateTimeOnly(lectureTimeFromHours);
                    if (!error.IsNullOrEmpty())
                    {
                        Console.WriteLine(error);
                    }

                } while (!error.IsNullOrEmpty());

                do
                {
                    Console.WriteLine("Įveskite paskaitos pabaigos laiką HH:MM");
                    lectureTimeToHours = Console.ReadLine();
                    error = validationService.ValidateTimeOnly(lectureTimeToHours);
                    if (!error.IsNullOrEmpty())
                    {
                        Console.WriteLine(error);
                    }

                } while (!error.IsNullOrEmpty());
                error = validationService.ValidateLectureFromToTime(lectureTimeFromHours, lectureTimeToHours);
                if (!error.IsNullOrEmpty())
                {
                    Console.WriteLine(error);
                }
            } while (!error.IsNullOrEmpty());

            do
            {
                Console.WriteLine("Įveskite paskaitos dieną");
                lectureDay = Console.ReadLine();
                error = validationService.ValidateLectureDay(lectureDay);
                if (!error.IsNullOrEmpty())
                {
                    Console.WriteLine(error);
                }

            } while (!error.IsNullOrEmpty());

            Lecture newLecture = new Lecture(null, lectureName, TimeOnly.Parse(lectureTimeFromHours), TimeOnly.Parse(lectureTimeToHours), lectureDay);
            lectureService.CreateLecture(newLecture);
        }

        public List<Student> PrintAllStudents()
        {
            var students = studentService.GetAllStudents();
            foreach (var student in students)
            {
                Console.WriteLine($"Numeris: {student.StudentNumber}, studento vardas: {student.FirstName}, pavardė: {student.LastName}, departamento kodas: {student.DepartmentCode}");
            }
            return students;
        }

        public List<Department> PrintAllDepartments()
        {
            var departments = departmentService.GetAllDepartments();
            foreach (var department in departments)
            {
                Console.WriteLine($"Kodas: {department.DepartmentCode}, departamento pavadinimas: {department.DepartmentName}");
            }
            return departments;
        }

        public void AddStudentToDepartment()
        {
            string error = string.Empty;
            string studentNumber;
            string departmentCode;
            PrintAllStudents();

            do
            {
                Console.WriteLine("Įveskite studento numerį");
                studentNumber = Console.ReadLine();
                error = validationService.ValidateStudentNumber(studentNumber);
                if (!error.IsNullOrEmpty())
                {
                    Console.WriteLine(error);
                } else
                {
                    var student = studentService.GetStudentByNumber(int.Parse(studentNumber));
                    if (student != null)
                    {
                        PrintAllDepartments();
                        do
                        {
                            Console.WriteLine("Įveskite departamento kodą");
                            departmentCode = Console.ReadLine();
                            var department = departmentService.GetDepartmentByCode(departmentCode);
                            if (department != null)
                            {
                                studentService.AddStudentToDepartment(departmentCode, student);
                                error = string.Empty;
                            } else
                            {
                                error = "Nerastas departamentas su tokiu kodu";
                                Console.WriteLine(error);
                            }
                        } while (!error.IsNullOrEmpty());
                    } else
                    {
                        error = "Tokio studento nėra";
                        Console.WriteLine(error);
                    }
                }
            } while (!error.IsNullOrEmpty());
        }

        public void PrintAllLectures()
        {
            var lectures = lectureService.GetAllLectures();
            foreach (var lecture in lectures)
            {
                Console.WriteLine($"Id: {lecture.LectureId}, pavadinimas: {lecture.LectureName}, nuo: {lecture.LectureTimeFrom}, iki: {lecture.LectureTimeTo}, diena: {GetLectureDay(lecture.LectureDay)}");
            }
        }

        public string GetLectureDay(string day)
        {
            if (day == null)
            {
                return "Monday,Tuesday,Wednesday,Thursday,Friday"; 
            }
            return day;
        }

        public void AddLectureToDepartment()
        {
            string error = string.Empty;
            string lectureId;
            string departmentCode;
            PrintAllLectures();
            do
            {
                Console.WriteLine("Įveskite paskaitos id");
                lectureId = Console.ReadLine();
                error = validationService.ValidateOnlyNumbers(lectureId);
                if (!error.IsNullOrEmpty())
                {
                    Console.WriteLine(error);
                }
                else
                {
                    var lecture = lectureService.GetLectureById(int.Parse(lectureId));
                    if (lecture != null)
                    {
                        PrintAllDepartments();
                        do
                        {
                            Console.WriteLine("Įveskite departamento kodą");
                            departmentCode = Console.ReadLine();
                            var department = departmentService.GetDepartmentByCode(departmentCode);
                            if (department != null)
                            {
                                departmentService.AddLectureToDepartment(department, lecture);
                                error = string.Empty;
                            }
                            else
                            {
                                error = "Nerastas departamentas su tokiu kodu";
                                Console.WriteLine(error);
                            }
                        } while (!error.IsNullOrEmpty());
                    }
                    else
                    {
                        error = "Paskaitos su tokiu id nėra";
                        Console.WriteLine(error);
                    }
                }
            } while (!error.IsNullOrEmpty());
        }


        public void AddLectureToStudent()
        {
            string error = string.Empty;
            string lectureId;
            string studentNumber;
            PrintAllLectures();
            do
            {
                Console.WriteLine("Įveskite paskaitos id");
                lectureId = Console.ReadLine();
                error = validationService.ValidateOnlyNumbers(lectureId);
                if (!error.IsNullOrEmpty())
                {
                    Console.WriteLine(error);
                }
                else
                {
                    var lecture = lectureService.GetLectureById(int.Parse(lectureId));
                    if (lecture != null)
                    {
                        PrintAllStudents();
                        do
                        {
                            Console.WriteLine("Įveskite studento numerį");
                            studentNumber = Console.ReadLine();
                            error = validationService.ValidateOnlyNumbers(studentNumber);
                            if (!error.IsNullOrEmpty())
                            {
                                Console.WriteLine(error);
                            }
                            else
                            {
                                var student = studentService.GetStudentByNumber(int.Parse(studentNumber));
                                if (student != null)
                                {
                                    lectureService.AddLectureToStudent(student, lecture);
                                    error = string.Empty;
                                }
                                else
                                {
                                    error = "Nerastas studentas su tokiu numeriu";
                                    Console.WriteLine(error);
                                }
                            }
                        } while (!error.IsNullOrEmpty());
                    }
                    else
                    {
                        error = "Paskaitos su tokiu id nėra";
                        Console.WriteLine(error);
                    }
                }
            } while (!error.IsNullOrEmpty());
        }
    }
}