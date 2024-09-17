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
            MemberName = $"{c.Member.FirstName} {c.Member.LastName}"; // Assuming Member has FirstName and LastName
            SessionsCount = c.Sessions?.Count ?? 0;
            isStandAlone = c.isStandAlone;
        }

        public int CourseId { get; set; }

        [Display(Description = "Course Name:")]
        public string Name { get; set; } = default!;

        [Display(Description = "Description:")]
        public string Description { get; set; } = default!;

        [Display(Description = "Created By (Member ID):")]
        public int MemberId { get; set; }

        [Display(Description = "Created By (Member Name):")]
        public string MemberName { get; set; } = default!;

        [Display(Description = "Number of Sessions:")]
        public int SessionsCount { get; set; }

        [Display(Description = "Is Standalone:")]
        public bool isStandAlone { get; set; }

        // Additional properties or calculations < to discuss in next code review if we would like to keep these.

        [Display(Description = "Course Complexity (based on name length):")]
        public int NameLength => Name.Length;

    }
}
