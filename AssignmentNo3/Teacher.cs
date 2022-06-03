using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentNo3
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string Qualification { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public void CreateTeacher()
        {
            Console.Write("Teacher Name:");
            string name = Console.ReadLine();
            Console.Write("Qualification:");
            string Qualification = Console.ReadLine();
            Console.Write("Address:");
            string address = Console.ReadLine();
            
            try
            {
                using DbEntities entities = new DbEntities();
                
                (string username, string password) LoginInformation = NecessaryMethod.LoginInformation();

                User user1 = new User()
                {
                    UserName = LoginInformation.username,
                    Password=LoginInformation.password,
                    RoleName=Roles.Teacher.ToString()
                };
                

                entities.Users.Add(user1);
                entities.SaveChanges();

                Teacher teacher = new Teacher()
                {
                    Address = address,
                    Qualification = Qualification,
                    Name = name,
                    UserId = user1.UserId
                };
                
                entities.Teachers.Add(teacher);
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

        
        public void AssignCourse()
        {
            Console.Write("Course Name:");
            string title = Console.ReadLine();

            using DbEntities entities = new DbEntities();

           var course=entities.Courses.Where(x=>x.Title.ToLower()==title.ToLower()).FirstOrDefault();
           
            if(course==null)
            {
                Console.WriteLine("Incorrect Course Name");
                AssignCourse();
            }

            Console.Write("Enter Teacher Name:");
            string teacherName = Console.ReadLine();
       
            Teacher teacher = entities.Teachers.Where(x =>
                       x.Name.ToLower() == teacherName.ToLower()).FirstOrDefault();

            if (teacher == null)
            {
                Console.WriteLine("Incorrect Teacher Name");
                AssignCourse();
            }
            course.TeacherId = teacher.TeacherId;

            if(GenericClass<Course>.Update(course))
            {
                Console.WriteLine($"Assiged Successfully course={course.Title} to" +
                    $" {teacher.Name}");
                new User().Options();
            }
            else
            {
                Console.WriteLine("Update Failed");
                AssignCourse();
            }
        }

        public void GetAttendenceReport(User user)
        {
            Console.Write("Course Name:");
            string title = Console.ReadLine();

            using DbEntities entities = new DbEntities();
            Course course =entities.Courses.Where(x=>x.Title.ToLower()
            ==title.ToLower()).Include(y=>y.Students).ThenInclude(z=>z.Student).FirstOrDefault();
            if (course == null)
            {
                Console.WriteLine("Incorrect Course");
                GetAttendenceReport(user);
            }
            Teacher teacher =entities.Teachers.Where(x=>x.UserId==
            user.UserId && x.TeacherId==course.TeacherId).FirstOrDefault();
            
            if(teacher == null )
            {
                Console.WriteLine("Incorrect Course");
                GetAttendenceReport(user);
            }
            Console.Write("Name    ");
            for (DateTime d = course.StartDate.GetValueOrDefault(); d <= DateTime.Now; d=d.AddDays(1))
            {
                Console.Write(d.ToString("dd-MM-yyyy")+" ");
            }
            Console.WriteLine();
            foreach (var item in course.Students)
            {
                Console.Write(item.Student.Name);
               
                for (DateTime d = course.StartDate.GetValueOrDefault(); d <= DateTime.Now;d= d.AddDays(1))
                {
                    Attendance attendance = entities.Attendances.Where(
                        x => x.Attendancedate.Day == d.Day
                        && x.Attendancedate.Month==d.Month
                        && x.Attendancedate.Year==d.Year).FirstOrDefault();
                    
                    if(attendance==null)
                        Console.Write("       X   ");
                    else
                        Console.Write("       ✓  ");
                   
                }
                Console.WriteLine();
            }


        }
    }
}
