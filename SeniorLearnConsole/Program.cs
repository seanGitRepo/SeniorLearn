using Microsoft.EntityFrameworkCore;
using SeniorLearnConsole.Data;
using Microsoft.EntityFrameworkCore;

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Identity.Client;

namespace SeniorLearnConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var context = new MyAppContext();



            login(context);




        }


        static void login(MyAppContext context)
        {
            Console.WriteLine("Please enter member username");
            string us = Console.ReadLine();
            var user = new Member();
            var test = true;
            var d = new Display();
            while (test)
            {
                try
                {
                 
                    user = context.Members.FirstOrDefault(m => m.FirstName == us);
                    test = false;
                }
                catch (Exception)
                {

                    Console.WriteLine("that user could not be found, please try again");
                    test = true;

                    us = Console.ReadLine();
                }


            }

            d.Menu(user,context);


        }


        static void seedDataBase()
            {

                var context = new MyAppContext();
                var create = new Create();
                var display = new Display();

                //members

                var m1 = new Member { FirstName = "sean", LastName = "saap" };
                var m2 = new Member { FirstName = "saxon", LastName = "cadet" };
                var m3 = new Member { FirstName = "corey", LastName = "irwin" };

                create.createMember(m1, context);
                create.createMember(m2, context);
                create.createMember(m3, context);

                var c1 = new Course { CourseName = "Gmail Crash Course", Description = "Beginners guide to gmail" };
                var c2 = new Course { CourseName = "Pro Chess Course", Description = "upskill a classic game" };

                create.createCourse(c1, context);
                create.createCourse(c2, context);

                for (var i = 0; i < 5; i++)
                {
                    var t = DateTime.Now.AddDays(i);


                    create.createSession(t, c1, context);

                }
                for (var i = 0; i < 5; i++)
                {
                    var t = DateTime.Now.AddDays(i + 3);


                    create.createSession(t, c2, context);

                }



            }




    }
}


