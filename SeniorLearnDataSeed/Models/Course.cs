using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorLearnDataSeed.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        // [ForeignKey("Member")]
        
        public int OrganiserId { get; set; }

        
        public MemberCourse MemberCourse{ get; set; }
        
        //public Member Member { get; set; }
        public List<Session> Sessions { get; set; }
        public bool isStandAlone { get; set; }
        

        public Course()
        {
            Sessions = new List<Session>();
        }
    }
    
}
