using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SeniorLearnConsole.Data
{
    public class Create
    {

        public void createCourse(Course c, MyAppContext con)
        {
            con.Courses.Add(c);
            con.SaveChanges();
        }

        public void createMember(Member m, MyAppContext con)
        {
            con.Members.Add(m);

            con.SaveChanges();

        }

        public void createSession(DateTime date, Course course, MyAppContext con)
        {

            var newSession = new Session { Date = date, Course = course };
            con.Sessions.Add(newSession);
            con.SaveChanges();
        }


        public void createMemberBooking(Member m, Course c, MyAppContext con)
        { 

            var newBooking = new MemberCourse { member = m,course = c};

            con.MemberSession.Add(newBooking);

            con.SaveChanges();


        }


    }

   



}
