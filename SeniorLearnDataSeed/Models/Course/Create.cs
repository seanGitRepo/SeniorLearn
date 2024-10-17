using SeniorLearnDataSeed.Data.Core;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ExceptionServices;

namespace SeniorLearnDataSeed.Models.Course
{
    public class Create
    {
        [Required]
        [Display(Name = "Course Name")]
        public string Name { get; set; } = default!;

        [Required]
        [Display(Name = "Course Description")]
        [StringLength(500, ErrorMessage = "The description cannot exceed 500 characters.")]
        public string Description { get; set; } = default!;

        [Required]
        [Display(Name = "Course Category")]
        [StringLength(30, ErrorMessage = "The category cannot exceed 30 characters.")]
        public string Category { get; set; } = default!;

        // This will link to the member who created the course (foreign key).
        [Required]
        [Display(Name = "Created By (Member ID)")]
        public string ApplicationUserId { get; set; }

        // This indicates whether the course is a standalone course or has sessions
        [Display(Name = "Is Standalone Course")]
        public bool isStandAlone { get; set; }

        // Default constructor
        public Create() { }

        // Constructor for initialization, if necessary (e.g., for pre-filling)
        public Create(Data.Core.Course course)
        {
            Name = course.Name;
            Description = course.Description;
            Category = course.Category;
            isStandAlone = course.isStandAlone;
        }

    }
}
