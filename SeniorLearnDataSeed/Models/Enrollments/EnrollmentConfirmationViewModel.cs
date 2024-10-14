namespace SeniorLearnDataSeed.Models.Enrollments
{
    public class EnrollmentConfirmationViewModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public int SessionId { get; set; }
        public DateTime SessionStartTime { get; set; }
        public DateTime SessionEndTime { get; set; }
    }
}
