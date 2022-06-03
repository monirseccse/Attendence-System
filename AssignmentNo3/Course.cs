using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentNo3
{
    public class Course
    {
        public int CourseId { get; set; }
        public DateTime? StartDate { get; set; }
        public string Title { get; set; }
        public double Fees { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public List<CourseStudent> Students { get; set; }
        
        public Course GetCourseDetails(string coursename)
        {
            using DbEntities entitie = new DbEntities();
            return entitie.Courses.Where(x=>x.Title.ToLower()==
            coursename.ToLower()).FirstOrDefault();
        }

        public void CreateCourse()
        {
            Console.Write("Course Name:");
            string title = Console.ReadLine();

            Console.Write("Fees:");
            string fee= Console.ReadLine();
            Console.Write("Start Date:");
            string startDate = Console.ReadLine();
            double.TryParse(fee,out double feeAmount);
           
                Console.Write("Enter Teacher Name:");
                string teacherName = Console.ReadLine();
                using DbEntities entitie = new DbEntities();
                Teacher teacher = entitie.Teachers.Where(x =>
                           x.Name.ToLower() == teacherName.ToLower()).FirstOrDefault();

                if (teacher == null)
                {
                    Console.WriteLine("Incorrect Teacher Name");
                    CreateCourse();
                }
               
            
            try
            {
                Course course = new Course
                {
                    Title = title,
                    Fees = feeAmount,
                    TeacherId = teacher.TeacherId,
                    StartDate= Convert.ToDateTime(startDate)

                };
                using DbEntities entities = new DbEntities();
                entities.Courses.Add(course);
                entities.SaveChanges();
                Console.WriteLine("Created Successfully");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error Occured");
            }
            finally
            {
                new User().UserMenu();
            }
           
           
        }
    }
}
