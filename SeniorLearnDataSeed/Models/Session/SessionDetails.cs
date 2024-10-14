using SeniorLearnDataSeed.Data.Core;
using System.ComponentModel.DataAnnotations;


namespace SeniorLearnDataSeed.Models.Session
{
    public class SessionDetails
    {
        [Key]
        public int SessionId { get; set; }

        public int? CourseId { get; set; }

        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }
        

        // public SessionStatus sessionStatus { get; set; }
        public string CourseName { get; set; }   
        public string eventLocation { get; set; }  

        public SessionDetails() { }

        public SessionDetails(Data.Core.Session session)
        {
            SessionId = session.SessionId;
            CourseId = session.CourseId;
            StartTime = session.StartTime;
            EndTime = session.EndTime;
            status = (SessionStatusModel)session.Status;
          
        }

        public SessionStatusModel status { get; set; }

    }

    public enum SessionStatusModel
    {
        Cancelled = 1,
        Scheduled = 2,
        Draft = 3,
        Complete = 4,
        Closed = 5

    }

  
}
