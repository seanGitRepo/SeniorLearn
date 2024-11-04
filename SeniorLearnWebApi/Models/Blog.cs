using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SeniorLearnWebApi.Models
{
    public class Blog
    {

        public Blog()
        {

        }
        [BsonElement("blog_id")]
        public ObjectId BlogId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ApplicationUserId { get; set; }
        public string? CreatorName { get; set; }
        public DateTime PostDate { get; set; } = DateTime.Now;
    }
}
