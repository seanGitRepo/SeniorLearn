namespace SeniorLearnWebApi.Models
{
    public class Blog
    {

        public Blog()
        {

        }

        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ApplicationUserId { get; set; }
        public string CreatorName { get; set; }
        public DateTime PostDate { get; set; } = DateTime.Now;
    }
}
