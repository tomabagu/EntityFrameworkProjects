using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFBaigiamasisStudentai.Database.Entities
{
    public class Lecture
    {
        public Lecture(int? lectureId, string? lectureName, TimeOnly lectureTimeFrom, TimeOnly lectureTimeTo, string? lectureDay)
        {
            LectureId = lectureId;
            LectureName = lectureName;
            LectureTimeFrom = lectureTimeFrom;
            LectureTimeTo = lectureTimeTo;
            LectureDay = lectureDay;
        }

        public int? LectureId { get; set; }
        public string? LectureName { get; set; }
        public TimeOnly LectureTimeFrom { get; set; }
        public TimeOnly LectureTimeTo { get; set; }
        public string? LectureDay { get; set; }
        public ICollection<Department> Departments { get; set; } = new List<Department>();
        public ICollection<Student> Students { get; set; } = new List<Student>();

    }
}
