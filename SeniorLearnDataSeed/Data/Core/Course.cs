using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorLearnDataSeed.Data.Core
{
    public class Course
    {
        // Unique identifier for all members (primary key)
        [Key]
        public int CourseId { get; set; }

        //TODO: why is "required" here but not in Member class?
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Difficulty { get; set; }
        public string CreatorName { get; set; }


        //Foreign Key for who crated the Course.
        //TODO: a way to switch course creators
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        //A one to many connection to list all the corresponding sessions with the course.
        public List<Session> Sessions { get; set; }

        //TODO: do we need this bool statment? as the count in the list above will be able to indicate if it is a standalone?
        public bool isStandAlone { get; set; }


        public Course()
        {
            Sessions = new List<Session>();
        }
    }

}

public static class Difficulty
{
    public const string AllLevels = "All Levels";
    public const string Beginner = "Beginner";
    public const string Intermediate = "Intermediate";
    public const string Advanced = "Advanced";
}


//TODO: On the calender, can filter through if the status of the course.

//TODO: Logic with the cookie with if u are a proemmeber you can create a course.


//TODO: useBoot strap is a complete ui overhall, pick the style.


//TODO: have discussion about ui, calender for the sessions.
