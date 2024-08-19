using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeniorLearnConsole.Data
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public List<Session> Sessions { get; set; }
        //many to many with Members.
        public List<MemberCourse> MemberCourse { get; set; }

        //one to may with organiser
        //public int proId { get; set; }
        //public ProMember ProMember { get; set; }
    }

    public class Session
    {
        public int sessionId { get; set; }
        public DateTime Date { get; set; }
        //public List<NormalMember> EnrolledMembers { get; set; }

        public int CourseId { get; set; }
        // one to many with courses
        public Course Course { get; set; }



    }


    public class MemberCourse
    {

        public int CourseId { get; set; }
        public int memberId { get; set; }

        public Member member { get; set; }
        public Course course { get; set; }
    }

}
