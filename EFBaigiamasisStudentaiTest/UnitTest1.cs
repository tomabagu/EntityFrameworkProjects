using EFBaigiamasisStudentai.Database;
using EFBaigiamasisStudentai.Database.Entities;
using EFBaigiamasisStudentai.Database.Interfaces;
using EFBaigiamasisStudentai.Database.Repositories;
using EFBaigiamasisStudentai.Interfaces;
using EFBaigiamasisStudentai.Services;
using Microsoft.EntityFrameworkCore;

namespace EFBaigiamasisStudentaiTest
{
    [TestClass]
    public class UnitTest1
    {
        private StudentaiContext _context;
        private DbContextOptions<StudentaiContext> _options;
        private IValidationService _validationService;
        private IDepartmentRepository departmentRepository;
        private ILectureRepository lectureRepository;
        private IStudentRepository studentRepository;
        private IDepartmentService departmentService;
        private ILectureService lectureService;
        private IStudentService studentService;


        [TestInitialize]
        public void OnInit()
        {
            _options = new DbContextOptionsBuilder<StudentaiContext>()
                .UseInMemoryDatabase(databaseName: "StudentasBaigiamasisDB" + Guid.NewGuid())
                .Options;
            _context = new StudentaiContext(_options);
            _context.Database.EnsureCreated();
            departmentRepository = new DepartmentRepository(_context);
            lectureRepository = new LectureRepository(_context);
            studentRepository = new StudentRepository(_context);
            _validationService = new ValidationService(_context,departmentRepository,lectureRepository,studentRepository);
            departmentService = new DepartmentService(departmentRepository, _validationService);
            studentService = new StudentService(studentRepository, _validationService);
            lectureService = new LectureService(lectureRepository, _validationService);
        }

        [TestCleanup]
        public void OnExit()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void CreateStudentNameOnlyLettersTest()
        {
            // Arrange
            var student = new Student(99999999,"Jo1n", "Smith","asd@asd.lt","ABC123");


            // Act
            string error = studentService.CreateStudent(student);

            // Assert
            Assert.AreEqual("Vardas turi būti sudarytas tik iš raidžių", error);
        }

        [TestMethod]
        public void CreateStudentNameSymbolLengthTest() //min 2 simboliai
        {
            // Arrange
            var student = new Student(99999999, "J", "Smith", "asd@asd.lt", "ABC123");


            // Act
            string error = studentService.CreateStudent(student);

            // Assert
            Assert.AreEqual("Studento vardas turi būti ne trumpesnis kaip 2 simboliai", error);
        }

        [TestMethod]
        public void CreateStudentNameSymbolMaxLengthTest() //51 raidė žodyje (max 50)
        {
            // Arrange
            var student = new Student(99999999, "JohnathonJohnathonJohnathonJohnathonJohnathonJohnat", "Smith", "asd@asd.lt", "ABC123");


            // Act
            string error = studentService.CreateStudent(student);

            // Assert
            Assert.AreEqual("Studento vardas turi būti ne ilgesnis kaip 50 simbolių", error);
        }

        //pavarde
        [TestMethod]
        public void CreateStudentLastNameOnlyLettersTest()
        {
            // Arrange
            var student = new Student(99999999, "John", "Sm1th", "asd@asd.lt", "ABC123");


            // Act
            string error = studentService.CreateStudent(student);

            // Assert
            Assert.AreEqual("Pavardė turi būti sudarytas tik iš raidžių", error);
        }

        [TestMethod]
        public void CreateStudentLastNameSymbolLengthTest() //min 2 simboliai
        {
            // Arrange
            var student = new Student(99999999, "John", "S", "asd@asd.lt", "ABC123");


            // Act
            string error = studentService.CreateStudent(student);

            // Assert
            Assert.AreEqual("Studento pavardė turi būti ne trumpesnis kaip 2 simboliai", error);
        }

        [TestMethod]
        public void CreateStudentLastNameSymbolMaxLengthTest() //51 raidė žodyje (max 50)
        {
            // Arrange
            var student = new Student(99999999, "Johnathon", "SmithSmithSmithSmithSmithSmithSmithSmithSmithSmithS", "asd@asd.lt", "ABC123");


            // Act
            string error = studentService.CreateStudent(student);

            // Assert
            Assert.AreEqual("Studento pavardė turi būti ne ilgesnis kaip 50 simbolių", error);
        }

        [TestMethod]
        public void CreateStudentNumber7LengthTest()
        {
            // Arrange
            var student = new Student(1234567, "Johnathon", "Smith", "asd@asd.lt", "ABC123");


            // Act
            string error = studentService.CreateStudent(student);

            // Assert
            Assert.AreEqual("Studento numeris turi būti tikslaiai 8 simbolių ilgio", error);
        }

        [TestMethod]
        public void CreateStudentNumber9LengthTest()
        {
            // Arrange
            var student = new Student(123456789, "Johnathon", "Smith", "asd@asd.lt", "ABC123");


            // Act
            string error = studentService.CreateStudent(student);

            // Assert
            Assert.AreEqual("Studento numeris turi būti tikslaiai 8 simbolių ilgio", error);
        }

        [TestMethod]
        public void CreateStudentAlreadyExistsStudentNumberTest()
        {
            // Arrange
            var student = new Student(12345678, "Johnathon", "Smith", "asd@asd.lt", "ABC123");


            // Act
            string error = studentService.CreateStudent(student);

            // Assert
            Assert.AreEqual("Studentas su tokiu numeriu jau egzistuoja", error);
        }

        [TestMethod]
        public void CreateStudentEmailMissingEtaTest()
        {
            // Arrange
            var student = new Student(12345679, "Johnathon", "Smith", "john.smithexample.com", "ABC123");


            // Act
            string error = studentService.CreateStudent(student);

            // Assert
            Assert.AreEqual("Netinkamas el pašto formatas", error);
        }

        [TestMethod]
        public void CreateStudentEmailMissingDomainTest()
        {
            // Arrange
            var student = new Student(12345679, "Johnathon", "Smith", "john.smith@", "ABC123");


            // Act
            string error = studentService.CreateStudent(student);

            // Assert
            Assert.AreEqual("Netinkamas el pašto formatas", error);
        }

        [TestMethod]
        public void CreateStudentEmailMissingPlaceTest()
        {
            // Arrange
            var student = new Student(12345679, "Johnathon", "Smith", "@example.com", "ABC123");


            // Act
            string error = studentService.CreateStudent(student);

            // Assert
            Assert.AreEqual("Netinkamas el pašto formatas", error);
        }

        [TestMethod]
        public void CreateStudentEmailMissingDomainEndingTest()
        {
            // Arrange
            var student = new Student(12345679, "Johnathon", "Smith", "john.smith@example", "ABC123");


            // Act
            string error = studentService.CreateStudent(student);

            // Assert
            Assert.AreEqual("Netinkamas el pašto formatas", error);
        }

        [TestMethod]
        public void CreateStudentEmailNullTest()
        {
            // Arrange
            var student = new Student(12345679, "Johnathon", "Smith", null, "ABC123");


            // Act
            string error = studentService.CreateStudent(student);

            // Assert
            Assert.AreEqual("El. paštas yra privalomas", error);
        }

        [TestMethod]
        public void CreateStudentEmailEmptyTest()
        {
            // Arrange
            var student = new Student(12345679, "Johnathon", "Smith", "", "ABC123");


            // Act
            string error = studentService.CreateStudent(student);

            // Assert
            Assert.AreEqual("El. paštas yra privalomas", error);
        }

        [TestMethod]
        public void CreateStudentEmailEmptySpacesTest()
        {
            // Arrange
            var student = new Student(12345679, "Johnathon", "Smith", "  ", "ABC123");


            // Act
            string error = studentService.CreateStudent(student);

            // Assert
            Assert.AreEqual("El. paštas yra privalomas", error);
        }

        [TestMethod]
        public void CreateStudentNameNullTest()
        {
            // Arrange
            var student = new Student(99999999, null, "Smith", "asd@asd.lt", "ABC123");


            // Act
            string error = studentService.CreateStudent(student);

            // Assert
            Assert.AreEqual("Studento vardas privalo būti nurodytas", error);
        }

        [TestMethod]
        public void CreateStudentNameEmptyTest()
        {
            // Arrange
            var student = new Student(99999999, "", "Smith", "asd@asd.lt", "ABC123");


            // Act
            string error = studentService.CreateStudent(student);

            // Assert
            Assert.AreEqual("Studento vardas privalo būti nurodytas", error);
        }

        [TestMethod]
        public void CreateStudentNameEmptySpacesTest()
        {
            // Arrange
            var student = new Student(99999999, "   ", "Smith", "asd@asd.lt", "ABC123");


            // Act
            string error = studentService.CreateStudent(student);

            // Assert
            Assert.AreEqual("Studento vardas privalo būti nurodytas", error);
        }

        [TestMethod]
        public void CreateLectureNameNullTest()
        {
            // Arrange
            var lecture = new Lecture(1, null, new TimeOnly(10,10), new TimeOnly(12, 10), "Friday");


            // Act
            string error = lectureService.CreateLecture(lecture);

            // Assert
            Assert.AreEqual("Paskaitos pavadinimas yra privalomas", error);
        }

        [TestMethod]
        public void CreateLectureNameEmptyTest()
        {
            // Arrange
            var lecture = new Lecture(1, "", new TimeOnly(10, 10), new TimeOnly(12, 10), "Friday");


            // Act
            string error = lectureService.CreateLecture(lecture);

            // Assert
            Assert.AreEqual("Paskaitos pavadinimas yra privalomas", error);
        }

        [TestMethod]
        public void CreateLectureNameEmptySpaceTest()
        {
            // Arrange
            var lecture = new Lecture(1, " ", new TimeOnly(10, 10), new TimeOnly(12, 10), "Friday");


            // Act
            string error = lectureService.CreateLecture(lecture);

            // Assert
            Assert.AreEqual("Paskaitos pavadinimas yra privalomas", error);
        }

        [TestMethod]
        public void CreateStudentEmailAlreadyExistsTest()
        {
            // Arrange
            var student = new Student(99999999, "Johnaton", "Smith", "alice.johnson@example.com", "ABC123");


            // Act
            string error = studentService.CreateStudent(student);

            // Assert
            Assert.AreEqual("Pažeistas studento unikalumas, toks el. paštas jau egzistuoja", error);
        }

        [TestMethod]
        public void CreateDepartmentNameSymbolLengthTest() //2 symbols (min 3 required)
        {
            // Arrange
            var department = new Department("CS1365", "CS");


            // Act
            string error = departmentService.CreateDepartment(department);

            // Assert
            Assert.AreEqual("Departamento pavadinimas turi būti ne trumpesnis kaip 3 simboliai", error);
        }

        [TestMethod]
        public void CreateDepartmentNameOnlyLettersAndNumbersTest()
        {
            // Arrange
            var department = new Department("CSS222", "Computer Science & Engineering");


            // Act
            string error = departmentService.CreateDepartment(department);

            // Assert
            Assert.AreEqual("Departamento pavadinimas gali būti tik raidės ir skaičiai",error);
        }

        [TestMethod]
        public void CreateDepartmentCodeSymbolLengthTest() //4 symbols (6 symbols required)
        {
            // Arrange
            var department = new Department("CS12", "Computers");


            // Act
            string error = departmentService.CreateDepartment(department);

            // Assert
            Assert.AreEqual("Departamento kodas turi būti 6 simbolių ilgio", error);
        }

        [TestMethod]
        public void CreateDepartmentCodeOnlyNumbersTest()
        {
            // Arrange
            var department = new Department("CS123@", "Computers");


            // Act
            string error = departmentService.CreateDepartment(department);

            // Assert
            Assert.AreEqual("Departamento kodas gali būti tik raidės ir skaičiai", error);
        }

        [TestMethod]
        public void CreateDepartmentCodeAlreadyExistsTest()
        {
            // Arrange
            var department = new Department("CS1234", "Computers");


            // Act
            string error = departmentService.CreateDepartment(department);

            // Assert
            Assert.AreEqual("Toks departmento kodas jau egzistuoja", error);
        }

        [TestMethod]
        public void AddLectureToDepartmentSuccessTest()
        {
            // Arrange
            var lecture = new Lecture(3, "DataStructures", new TimeOnly(14, 00), new TimeOnly(15, 30), null);
            var department = new Department("MTH567", "Mathematics");



            // Act
            string error = departmentService.AddLectureToDepartment(department,lecture);

            // Assert
            Assert.AreEqual("", error);
        }

        [TestMethod]
        public void AddLectureToDepartmentAlreadyAssignedTest()
        {
            // Arrange
            var lecture = new Lecture(3, "DataStructures", new TimeOnly(14, 00), new TimeOnly(15, 30), null);
            var department = new Department("CS1234", "Mathematics");



            // Act
            string error = departmentService.AddLectureToDepartment(department, lecture);

            // Assert
            Assert.AreEqual("Tokia paskaita departamente jau yra", error);
        }

        [TestMethod]
        public void CreateLectureNameLengthTest() //4 symbols (required min 5)
        {
            // Arrange
            var lecture = new Lecture(1, "Math", new TimeOnly(10, 10), new TimeOnly(12, 10), "Friday");


            // Act
            string error = lectureService.CreateLecture(lecture);

            // Assert
            Assert.AreEqual("Paskaitos pavadinimas turi būti ne trumpesnis kaip 5 simboliai", error);
        }

        //Neina ištestuoti nes TimeOnly nepriima jau ir taip neteisingo laiko
        /*[TestMethod]
        public void CreateLectureWrongLectureTimeTest() 
        {
            // Arrange
            var lecture = new Lecture(1, "Math", new TimeOnly(25, 10), new TimeOnly(26, 10), "Friday");


            // Act
            string error = lectureService.CreateLecture(lecture);

            // Assert
            Assert.AreEqual("Pradžios ir pabaigos laikas turi būti tarp 00:00 ir 24:00", error);
        }*/

        [TestMethod]
        public void CreateLectureFromTimeAfterToTimeTest() 
        {
            // Arrange
            var lecture = new Lecture(1, "Mathematics", new TimeOnly(14, 00), new TimeOnly(13, 10), "Friday");


            // Act
            string error = lectureService.CreateLecture(lecture);

            // Assert
            Assert.AreEqual("Pabaigos laikas negali būti ankstesnis už pradžios laiką", error);
        }

        [TestMethod]
        public void CreateLectureWrongDayTest()
        {
            // Arrange
            var lecture = new Lecture(1, "Mathematics", new TimeOnly(10, 00), new TimeOnly(13, 10), "Sunday");


            // Act
            string error = lectureService.CreateLecture(lecture);

            // Assert
            Assert.AreEqual("Savaitės diena gali būti tik Monday, Tuesday, Wednesday, Thursday, Friday", error);
        }

        [TestMethod]
        public void AssignStudentToDepartmentDepartmentNotExistTest()
        {
            // Arrange
            var student = new Student(12345679, "Johnaton", "Smith", "alice.johnson@example.com", "ABC123");
            var department = new Department("ENG999", "Mathematics");


            // Act
            string error = studentService.AddStudentToDepartment(department.DepartmentCode, student);

            // Assert
            Assert.AreEqual("Depapartment code does not exist", error);
        }

        [TestMethod]
        public void AssignStudentStudentLectureDoesNotBelongToDepartmentTest()
        {
            // Arrange
            var student = new Student(12345678, "Johnaton", "Smith", "alice.johnson@example.com", "ABC123");
            var lecture = new Lecture(2, "Calculus", new TimeOnly(12, 00), new TimeOnly(13, 30), null);


            // Act
            string error = lectureService.AddLectureToStudent(student, lecture);

            // Assert
            Assert.AreEqual("Paskaita nepriklauso departmentui", error);
        }

        [TestMethod]
        public void ChangeStudentDepartmentSuccessTest()
        {
            // Arrange
            var student = new Student(12345678, "Johnatonas", "Smith", "alice.johnson@example.com", "ABC123");
            var department = new Department("MTH567", "Mathematics");


            // Act
            string error = studentService.AddStudentToDepartment(department.DepartmentCode, student);

            // Assert
            Assert.AreEqual("", error);
        }

        [TestMethod]
        public void ChangeStudentDepartmentNotExistDepartmentTest()
        {
            // Arrange
            var student = new Student(12345678, "Johnatonas", "Smith", "alice.johnson@example.com", "ABC123");
            var department = new Department("ENG999", "Mathematics");


            // Act
            string error = studentService.AddStudentToDepartment(department.DepartmentCode, student);

            // Assert
            Assert.AreEqual("Depapartment code does not exist", error);
        }

    }
}