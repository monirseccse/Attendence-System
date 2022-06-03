using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentNo3
{
    public class CourseStudent
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public DateTime EnrollDate { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }

        public void AssignCourseToStudent()
        {
            Console.Write("please enter course name:");
            string coursename =Console.ReadLine();
            Course course =new Course().GetCourseDetails(coursename);
            if(course==null)
            {
                Console.WriteLine("Incorrect Course Name");
                AssignCourseToStudent();
            }
            Console.Write("please enter Student name:");
            string studentname = Console.ReadLine();

            Student student =new Student().GetStudentDetails(studentname);

            CourseStudent courseStudent = new CourseStudent
            {
                CourseId =course.CourseId,
                StudentId =student.StudentId,
                EnrollDate=DateTime.Now
            };
            
            try
            {
                using DbEntities entitie = new DbEntities();
                entitie.CourseStudents.Add(courseStudent);
                entitie.SaveChanges();
                Console.WriteLine("Saved Successfully");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error Occured");
            }
            finally
            {
                new User().Options();
            }

            
        }
    }
}
