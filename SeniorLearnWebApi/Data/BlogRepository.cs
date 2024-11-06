using MongoDB.Bson;
using MongoDB.Driver;
using SeniorLearnWebApi.Controllers;
using SeniorLearnWebApi.Models;
using System.Reflection;
namespace SeniorLearnWebApi.Data

{
    public class BlogRepository
    {
        private readonly IMongoCollection<Blog> _blogsCollection;
        public BlogRepository(IConfiguration config)
        {
            var connectionString = config.GetConnectionString("MongoDb");
            var client = new MongoClient(connectionString);
            _blogsCollection = client
                .GetDatabase("SeniorLearnDB")
                .GetCollection<Blog>("Blogs");
        }



        private static readonly List<Blog> blogs = new List<Blog>()
        {
            new Blog()
            {
                Title ="Test",
                Description="Test",

            },
            new Blog()
            {
                Title ="Test2",
                Description="Testing"
            }

        };
        

        public IEnumerable<Blog> GetAll()
        {

            List<Blog> blogGetAll = _blogsCollection.Find(_ => true).ToList();
            return blogGetAll;
        }

        public Blog? GetById(ObjectId Id)
        {
            var blog = blogs.Where(b => b.BlogId == Id).FirstOrDefault();

            return blog;
        }

        public void Save(Blog blog)
        {
            //blog.BlogId = GetNextId();
            blogs.Add(blog);
            _blogsCollection.InsertOne(blog);
        }

        public void Update(Blog blog)
        {
            var blogToUpdate = blogs
                .Where(b => b.BlogId == blog.BlogId)
                .FirstOrDefault();
            

            if(blogToUpdate != null)
            {
                blogToUpdate.Title = blog.Title;
                blogToUpdate.Description = blog.Description;

                var filter = Builders<Blog>.Filter.Eq(b => b.BlogId, blog.BlogId);

                var update = Builders<Blog>.Update
                    .Set(b => b.Title, blog.Title)
                    .Set(b => b.Description, blog.Description);

                var result = _blogsCollection.UpdateOne(filter, update);

                if(result.ModifiedCount == 0)
                {
                    throw new Exception("Blog update failed");
                }
            }
            else
            {
                throw new Exception("Bog not found.");
            }
        }

        public void Delete(ObjectId Id)
        {
            var result = _blogsCollection.DeleteOne(b => b.BlogId == Id);  // if BlogId is the MongoDB identifier
            if (result.DeletedCount == 0)
            {
                throw new Exception("Blog with the specified ID not found in the database.");
            }
        }

        public ObjectId GetNextId()
        {
            ObjectId currentId = blogs.Select(b => b.BlogId)
                .Max();

            return currentId;
            //need to add to this
        }
    }
}
