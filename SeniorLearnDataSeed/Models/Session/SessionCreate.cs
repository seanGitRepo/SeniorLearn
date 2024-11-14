using Microsoft.AspNetCore.Mvc.Rendering;
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

        public string SelectedStatus { get; set; }
       
     
        public string? session_type { get; set; }

        [StringLength(50)]
        public string? StreetName { get; set; } = null;

        [StringLength(4)] // can use these to set a bunch of rules in the form.
        public string? Suburb { get; set; } = null;

        [StringLength(10)]
        public string? StreetNumber { get; set; } = null;


        public string? MeetingLink { get; set; } = null;

        
        public int? Frequency { get; set; } = null;
        

        public TimePeriodFrequency SelectedTimePeriod { get; set; }
        
        public List<SelectListItem> TimePeriods { get; set; }

        


        

    }
    public enum TimePeriodFrequency
    {
        Month,
        Week,
        Day

    }


}
