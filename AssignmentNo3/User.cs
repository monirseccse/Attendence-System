using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentNo3
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
       

        public void Options()
        {
            Console.WriteLine("1. Create User Or Course");
            Console.WriteLine("2.Assign Teacher to Course");
            Console.WriteLine("3.Assign student to Course");
            Console.WriteLine("4. Set Course Schedule");
            Console.WriteLine("5.Exit");
            string value =Console.ReadLine();
          int typed=  NecessaryMethod.ConvertTypedValue(value);
            if(typed==0)
            {
                Console.WriteLine("Digit Only Allowed for further access");
                Options();
            }
            else
            {
                if(typed==1)
                {
                    CreateUser();
                }

                else if(typed==2)
                {
                    new Teacher().AssignCourse();
                }
                else if(typed==3)
                {
                    new CourseStudent().AssignCourseToStudent();
                }
                else if(typed==4)
                {
                    new CourseSchedule().SetCourseSchedule();

                }
            }

        }

       public void CreateUser()
        {
            Console.WriteLine("1.Create Teacher");
            
            Console.WriteLine("2.Create Student");
            Console.WriteLine("3.Create Course");

            string value =Console.ReadLine();
            int typed =NecessaryMethod.ConvertTypedValue(value);

            if(typed==0)
            {
                Console.WriteLine("Only Digit Value Allowed");
                CreateUser();
            }else
            {
                if(typed==1)
                {
                    new Teacher().CreateTeacher();
                }
                else if(typed==2)
                {
                    new Student().CreateStudent();
                }
                else if(typed==3)
                {
                    new Course().CreateCourse();
                }
            }
        }
        public void UserMenu()
        {
            Console.WriteLine("1. Create User Or Course");
            Console.WriteLine("2. Main Menu");
            string value = Console.ReadLine();
            int typed = NecessaryMethod.ConvertTypedValue(value);
            if (typed == 1)
            {
                new User().CreateUser();
            }
            else
            {
                new User().Options();
            }
        }
        public void ValidityCheck()
        {
            (string username, string password) LoginInformation =NecessaryMethod.LoginInformation();
          
            using DbEntities enitities =new DbEntities();
            User user = enitities.Users.Where(x => x.UserName.ToLower()
            == LoginInformation.username.ToLower()
            && x.Password == LoginInformation.password).FirstOrDefault();

            if (user == null)
            {
                Console.WriteLine("Incorrect User Name or Password");
                ValidityCheck();
            }
            else
            {
                Console.WriteLine("Login Successful");
                if(user.RoleName==Roles.Admin.ToString())
                {
                    Options();
                }
                else if(user.RoleName==Roles.Student.ToString())
                {
                    new Student().GetRunningCourseForAttendence(user);
                }
                else if(user.RoleName==Roles.Teacher.ToString())
                {
                    new Teacher().GetAttendenceReport(user);
                }

            }
        }
    }
}
