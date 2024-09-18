using System.ComponentModel.DataAnnotations;


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
            MemberId = c.MemberId;
            //MemberName = $"{c.Member.FirstName} {c.Member.LastName}"; // Assuming Member has FirstName and LastName
            //SessionsCount = c.Sessions?.Count ?? 0;
            isStandAlone = c.isStandAlone;
        }
        [Key]
        public int CourseId { get; set; }

        
        public string Name { get; set; } = default!;

       
        public string Description { get; set; } = default!;

        
        public int MemberId { get; set; }

     //   [Display(Description = "Created By (Member Name):")]
      //  public string MemberName { get; set; } = default!;

        //public int SessionsCount { get; set; }

       
        public bool isStandAlone { get; set; }

        // Additional properties or calculations < to discuss in next code review if we would like to keep these.

      //  public int NameLength => Name.Length;

    }
}
