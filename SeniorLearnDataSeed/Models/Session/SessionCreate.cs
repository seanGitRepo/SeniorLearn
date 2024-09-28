using SeniorLearnDataSeed.Data.Core;
using System.ComponentModel.DataAnnotations;

namespace SeniorLearnDataSeed.Models.Session
{
    public class SessionCreate
    {


        
        public int? CourseId { get; set; }

        [Required]
        public DateTime StartTime { get; set; } = DateTime.Now;

        [Required]
        public DateTime EndTime { get; set; }= DateTime.Now;

        public int status { get; set; }

        public string? session_type { get; set; }

        public string? location { get; set; } = null;

        public string? MeetingLink { get; set; } = null;




    }

  
}
