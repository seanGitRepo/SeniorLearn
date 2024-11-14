using System;

namespace SeniorLearnDataSeed.Models.Enrollments
{
    public class EnrollmentRepository
    {
        public EnrollmentRepository() { }

        public EnrollmentRepository(Data.Core.Enrollment e)
        {
            EnrollmentId = e.EnrollmentId;
            ApplicationUserId = e.ApplicationUserId;
            SessionId = e.SessionId;
            EnrolledDate = e.EnrolledDate;
            standalone = e.Session.Course.isStandAlone; // Assuming `IsStandalone` is available in Session
            CourseName = e.Session.Course.Name;  // Assuming Course and Name are accessible
            UserName = e.ApplicationUser.UserName; // Assuming ApplicationUser and UserName are accessible
        }

        public int EnrollmentId { get; set; }
        public string ApplicationUserId { get; set; }
        public int SessionId { get; set; }
        public bool standalone { get; set; }
        public DateTime EnrolledDate { get; set; }
        public string CourseName { get; set; }
        public string UserName { get; set; }
    }
}
