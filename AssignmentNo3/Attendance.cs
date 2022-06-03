using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentNo3
{
    public class Attendance
    {
        public int AttendanceId { get; set; }
        public DateTime Attendancedate { get; set; }
        public string AttendanceTime { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
