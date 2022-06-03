using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentNo3
{
    public class CourseSchedule
    {
        public int CourseScheduleId { get; set; }
        public string ScheduleDay { get; set; }

        public string StartFrom { get; set; }
        public string FinishTo { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public void SetCourseSchedule()
        {
            Console.Write("Enter Course Name:");
            string name = Console.ReadLine();
            Course course = new Course().GetCourseDetails(name);
            if (course == null)
            {
                Console.WriteLine("Incorrect Course Name");
                SetCourseSchedule();
            }
            Console.Write("Enter day Name:");
            string day = Console.ReadLine();
            Console.Write("Enter Start time:");
            string Start = Console.ReadLine();
            Console.Write("Enter Finish time:");
            string Finish = Console.ReadLine();

            CourseSchedule courseSchedule = new CourseSchedule
            {

                CourseId = course.CourseId,
                ScheduleDay = day,
                StartFrom = Start,
                FinishTo = Finish

            };

            try
            {
                using DbEntities entities = new DbEntities();
                entities.CourseSchedules.Add(courseSchedule);
                entities.SaveChanges();
                Console.WriteLine("Saved Successfully");
                new User().Options();
            }catch(Exception ex)
            {
                Console.WriteLine("Error Occured");
            }
        }

    }
}
