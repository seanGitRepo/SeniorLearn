using SeniorLearnDataSeed.Data.Core;
using System.ComponentModel.DataAnnotations;


namespace SeniorLearnDataSeed.Models.Session
{
    public class Details
    {
        [Key]
        public int SessionId { get; set; }

        public int? CourseId { get; set; }

        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

        public SessionStatus sessionStatus { get; set; }

        public Details() { }

        public Details(Data.Core.Session session)
        {
            SessionId = session.SessionId;
            CourseId = session.CourseId;
            StartTime = session.Date;
            sessionStatus = (SessionStatus)session.Status; ;
        }


    }

     public enum SessionStatus
    {
        Cancelled = 1,
        Scheduled = 2,
        Draft= 3,
        Complete= 4,
        Closed =5

    }
}
