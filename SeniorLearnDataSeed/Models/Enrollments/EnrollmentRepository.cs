using SeniorLearnDataSeed.Data.Core;

namespace SeniorLearnDataSeed.Models.Enrollments
{
    public class EnrollmentRepository
    {
        //for data separation
        public EnrollmentRepository(Data.Core.Enrollment e) {
            EnrollmentId = e.EnrollmentId;
            ApplicationUserId = e.ApplicationUserId;
            SessionId = e.SessionId;
        }
        public int EnrollmentId { get; set; }
        public string ApplicationUserId { get; set; }

        

        public int SessionId { get; set; }
        

        public DateTime EnrolledDate = DateTime.Now;
    }
}
