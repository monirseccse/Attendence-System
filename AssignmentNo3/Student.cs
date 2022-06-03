using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentNo3
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public virtual List<CourseStudent> Courses { get; set; }

       
        public void CreateStudent()
        {
            Console.Write("Student Name:");
            string name = Console.ReadLine();
            
            Console.Write("Address:");
            string address = Console.ReadLine();
            
          
            try
            {
                using DbEntities entities = new DbEntities();
               
                (string username, string password) LoginInformation = NecessaryMethod.LoginInformation();

                User user1 = new User()
                {
                    UserName = LoginInformation.username,
                    Password = LoginInformation.password,
                    RoleName = Roles.Student.ToString(),
                   
                };
                entities.Users.Add(user1);
                entities.SaveChanges();

                Student student = new Student()
                {
                    Address = address,

                    Name = name,
                    UserId = user1.UserId
                };

                

                entities.Students.Add(student);
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

        public Student GetStudentDetails(string studentname)
        {
            using DbEntities entities = new DbEntities();
            return entities.Students.Where(x=>x.Name.ToLower()
            ==studentname.ToLower()).FirstOrDefault();
        }
        
        public void GetRunningCourseForAttendence(User user)
        {
            using DbEntities entities = new DbEntities();
            Student student = entities.Students.Where(x=>x.UserId
            ==user.UserId).Include(y=>y.Courses).FirstOrDefault();
            bool flag = false;
            foreach(var item in student.Courses)
            {
               List<CourseSchedule> schedule =entities.CourseSchedules.Where(x=>x.CourseId
                ==item.CourseId).Include(y=>y.Course).ToList();
                
                foreach(var it in schedule)
                {
                    if (it.ScheduleDay == DateTime.Now.DayOfWeek.ToString()
                    && Convert.ToDateTime(it.StartFrom) <= DateTime.Now
                    && Convert.ToDateTime(it.FinishTo) >= DateTime.Now)
                    {
                        
                        Console.WriteLine($"Response will recorded for: {it.Course.Title} course");
                        Console.Write("Enter Name:");
                        string p=Console.ReadLine();
                        Attendance attendance = new Attendance
                        {
                            Attendancedate=DateTime.Now,
                            StudentId=student.StudentId,
                            CourseId=it.CourseId,
                            AttendanceTime=DateTime.Now.ToString("hh mm tt")
                        };

                        try
                        {
                            entities.Attendances.Add(attendance);
                            entities.SaveChanges();
                            flag = true;
                            Console.WriteLine("Respond received");
                            return;
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine("Error Occured");

                        }
                        
                    }
                }
            }
            if(!flag)
            {
                Console.WriteLine("Nothing found to response");
            }
        }
    }
}
