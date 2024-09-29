using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorLearnDataSeed.Data.Core
{
    public class Session
    {
        [Key]
        public int SessionId { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
        //Many to Many relationship with members through Enrollment class below.
        public List<Enrollment> EnrolledMembers { get; set; }

        public int? CourseId { get; set; }
        public Course Course { get; set; }

        public SessionStatus Status { get; set; }
      
        public Session()
        {

            EnrolledMembers = new List<Enrollment>();
        }

    }
    
    public class OnlineSession : Session
    {
        public string OnlineLink { get; set; }
        public string? MeetingCode { get; set; }
        public string AdditionalDetails { get; set; }
    }


    public class OnPremSession : Session
    {
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string Suburb { get; set; }


    }
  
    public enum SessionStatus
    {
        Cancelled = 1,
        Scheduled = 2,
        Draft = 3,
        Complete=4,
        Closed =5

    }

    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public int SessionId { get; set; }
        public Session Session { get; set; }

        public DateTime EnrolledDate = DateTime.Now;
    }


}
