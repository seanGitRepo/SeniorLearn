using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeniorLearnConsole.Data
{
    public class Edit
    {

        public void userAddCourse(Member user, MyAppContext context)
        {
            var x = new Display();
            var create = new Create();
            var test = true;
            while (test)
            {
                Console.WriteLine("\t choose from avaliable courses, using the ID and press enter");
                x.displayCourses(user.memberId, context);

                string userChoice = Console.ReadLine();
                try
                {
                   var c = context.Courses.FirstOrDefault(c => c.CourseId == int.Parse(userChoice));

                    if (c!= null)
                    {
                        create.createMemberBooking(user, c, context);
                        test = false;
                    }
                    else
                    {
                        test = true;
                    }

                    //TODO: add some fancy would you like to confirm?
                   

                }
                catch (Exception)
                {

                    Console.WriteLine("That is not a valid course, please try again, press enter");
                    Console.ReadLine();

                    test = true;

                }



                Console.Clear();
            }


        }

    }
}
