using Microsoft.AspNetCore.Identity;
using SeniorLearnDataSeed.Data.Core;
using System.ComponentModel.DataAnnotations;

namespace SeniorLearnDataSeed.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }


        
        public List<Course>? CreatedCourses { get; set; }

        //The Type class controls the "level" a member will have. as per the instructions from the client with "honoary/member/promember"
        public MemberType Type { get; set; }


        public List<Payment>? Payments { get; set; }

        //This variable will be needed to monitor when they started and when their due date is required for payment.
        public DateTime StartDate { get; } = DateTime.Now;

        //Enrollments will display and keep track of the members currenly enrolled sesssions they wish to attend.
        public List<Enrollment>? Enrollments { get; set; }
    }
}
