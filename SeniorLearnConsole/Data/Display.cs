using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace SeniorLearnConsole.Data
{
    public class Display
    {

        public void Menu(Member user, MyAppContext context)
        {
            var e = new Edit();
            var x = new Display();
            var test = true;


            while (test)
            {
                Console.WriteLine($"\t \t \t \t {user.FirstName} {user.LastName} Menu");
                Console.WriteLine();
                Console.WriteLine("\t \t 1. \t Enrolled Sessions");
                Console.WriteLine();
                Console.WriteLine("\t \t 2. \t Enrolled Courses");
                Console.WriteLine();
                Console.WriteLine("\t \t 3. \t Availiable Courses");
                Console.WriteLine();
                Console.WriteLine("\t \t 4. \t Enrol in new Course");

                string option = Console.ReadLine();




                if (option == "1")
                {
                    x.displayMembersSessions(user.memberId, context);

                }
                else if (option == "2")
                {
                    x.displayMembersCourses(user.memberId, context);

                }
                else if (option == "3")
                {
                    x.displayCourses(user.memberId, context);

                }
                else if (option == "4")
                {
                    e.userAddCourse(user, context);
                }
                else if (option == "q")
                {

                    test = false;

                }
                else
                {
                    test = true;
                }
                Console.WriteLine("enter to continue");
                Console.ReadLine();
                Console.Clear();
            }




        }








        // i shouldnt have to be doing this, why doesn't ef se the connection? 
        public void displayMembersSessions(int memberId, MyAppContext con)
        {
           

            var m = con.MemberSession
                .Where(b => b.memberId == memberId)
                .Include(b=> b.course)
                .Include(b => b.course.Sessions)
                .ToList();


            foreach (var item in m)
            {
                Console.WriteLine();

                Console.WriteLine($"\t  {item.course.CourseName}");



                foreach (var seh in item.course.Sessions)
                {
                   
                    Console.WriteLine();
                   
                    Console.WriteLine($"\t \t {seh.sessionId} \t {seh.Date}");
                    
                }

            }

            
           
        }

        public void displayMembersCourses(int memberId, MyAppContext con)
        {


            var m = con.MemberSession
                .Where(b => b.memberId == memberId)
                .Include(b => b.course)
                .Include(b => b.course.Sessions)
                .ToList();


            foreach (var item in m)
            {
                Console.WriteLine();

                Console.WriteLine($"\t \t {item.course.CourseName}");

            }

           
            
        }
        public void displayCourses(int memberId, MyAppContext con)
        {
            // code will not show courses that do not have sessions

            var m = con.MemberSession
                .Where(b => b.memberId == memberId)
                .Include(b => b.course)
                .Include(b => b.course.Sessions)
                .ToList();


            var cList = con.Courses.ToList();

            var dList = new List<Course>();

            foreach (var item in m)
            {
                dList.Add(item.course);
      
            }
            
            var notIn = cList.Except(dList);


            foreach (var item in notIn)
            {
                Console.WriteLine();
           
                Console.WriteLine($"\t {item.CourseId} \t \t {item.CourseName}");

            }
            


           
          
        }

    }
}
