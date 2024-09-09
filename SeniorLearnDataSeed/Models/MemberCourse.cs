namespace SeniorLearnDataSeed.Models
{
    public class MemberCourse
    {
        public int OrganiserId { get; set; }
        public Member Organiser { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

    }
}
