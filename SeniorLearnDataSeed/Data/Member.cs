using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace SeniorLearnDataSeed.Data
{
    public class Member
    {
        // Unique identifier for all members (primary key)
        [Key]
        public int MemberId { get; set; } 
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string Email { get; set; }


        //All members will have a createdcourses list, however not all members will need to have the correct status in order to create a course.
        public List<Course>? CreatedCourses { get; set; }  

        //The Type class controls the "level" a member will have. as per the instructions from the client with "honoary/member/promember"
        public MemberType Type { get; set; }


        public List<Payment> Payments { get; set; }

        //This variable will be needed to monitor when they started and when their due date is required for payment.
        public DateTime StartDate { get; set; }

        //Enrollments will display and keep track of the members currenly enrolled sesssions they wish to attend.
        public List<Enrollment> Enrollments { get; set; }


        public Member()
        {

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
