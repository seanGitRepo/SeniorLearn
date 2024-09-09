using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace SeniorLearnDataSeed.Models
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public List<Course> EnrolledCourses { get; set; }

        public List<MemberCourse>? CreatedCourses { get; set; }
        public MemberType Type { get; set; }
        public List<Payment> Payments {  get; set; }
        
        public DateTime StartDate { get; set; }
        public List<Enrollment> Enrollments { get; set; }


        public Member()
        {
            EnrolledCourses = new List<Course>();
            Enrollments = new List<Enrollment>();
            Payments = new List<Payment>();
        }

    }

    public enum MemberType
    {
        Pro,
        Standard,
        Honourary
    }

   
}
