using System.ComponentModel.DataAnnotations;

namespace SeniorLearnDataSeed.Models.Course
{
    public class Edit:Create
    {
        [Key]
        public int CourseId { get; set; }

      
    }
}
