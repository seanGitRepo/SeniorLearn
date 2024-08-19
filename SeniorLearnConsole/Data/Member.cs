
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SeniorLearnConsole.Data
{
    public class Member
    {
      
        public int memberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<MemberCourse> MemberCourse { get; set; }

            

    }

    //public class ProMember : Member
    //{

    //    public ProMember() { }
    //    //TODO: add roles

    //    public int proId { get; set; }

    //    public List<Course> CreatedCourses { get; set; }

    //    //public Course CreateCourse(string name, string description, ProMember proMember)
    //    //{

    //    //    if(string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
    //    //    {
    //    //        throw new ArgumentException("Course name or description cannot be empty.");
    //    //    }
    //    //    Course course = new Course { Name = name, Description = description, Organiser = proMember };
    //    //    CreatedCourses.Add(course);
    //    //    //course.Sessions.Add(new Session { Date = sessionDate});
    //    //    return course;
    //    //}

    //}

    
   


    
}
