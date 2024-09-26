using System.ComponentModel.DataAnnotations;
using SeniorLearnDataSeed.Data.Core;
using SeniorLearnDataSeed.Models.Session;

namespace SeniorLearnDataSeed.Models.Course
{
    public class Details
    {
        public Details() { }

        public Details(Data.Core.Course c)
        {
            CourseId = c.CourseId;
            Name = c.Name;
            Description = c.Description;
            ApplicationUserId = c.ApplicationUserId;

            Sessions = c.Sessions.Select(s => new Session.SessionDetails(s)).ToList();
           // Assuming Member has FirstName and LastName
            //SessionsCount = c.Sessions?.Count ?? 0;
            isStandAlone = c.isStandAlone;

        }
        [Key]
        public int CourseId { get; set; }

        public string Name { get; set; } = default!;
        // public List<Session> Sessions { get; set; } 

        public string Description { get; set; } = default!;

        
        public string ApplicationUserId { get; set; }

        //   [Display(Description = "Created By (Member Name):")]
         public string MemberName { get; set; } 

        //public int SessionsCount { get; set; }

        public List<Models.Session.SessionDetails> Sessions { get; set; }
        public bool isStandAlone { get; set; }

        // Additional properties or calculations < to discuss in next code review if we would like to keep these.

        //  public int NameLength => Name.Length;




    }
}


