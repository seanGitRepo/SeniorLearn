using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorLearnDataSeed.Models
{
    public class Session
    {
        [Key]
        public int SessionId { get; set; }
        public  DateTime Date {  get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        
        public List<Enrollment> EnrolledMembers {  get; set; }

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
        Cancelled, 
        Scheduled,
        Draft, 
        Complete, 
        Closed

    }

    public class Enrollment
    {
        
        public int MemberId { get; set; }
        
        public Member Member { get; set; }

        public int SessionId { get; set; }
        public Session Session { get; set; }
        public DateTime EnrolledDate = DateTime.Now;
    }


}
