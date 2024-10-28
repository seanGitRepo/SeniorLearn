using MongoDB.Driver;
using SeniorLearnWebApi.Controllers;
using SeniorLearnWebApi.Models;
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
                BlogId = 1,
                Title = "Test",
                Description ="Test"

            }
        };

        public IEnumerable<Blog> GetAll()
        {
            return blogs;
        }

        public Blog? GetById(int Id)
        {
            var blog = blogs.Where(b => b.BlogId == Id).FirstOrDefault();

            return blog;
        }

        public void Save(Blog blog)
        {
            blog.BlogId = GetNextId();
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

        public void Delete(int Id)
        {
            int index = blogs.FindIndex(b => b.BlogId == Id);
            if(index == -1)
            {
                throw new Exception();
            }
            else
            {
                blogs.RemoveAt(index);
                _blogsCollection.DeleteOne(b => b.BlogId == Id);
            }
        }

        public int GetNextId()
        {
            int currentId = blogs.Select(b => b.BlogId)
                .Max();

            return currentId + 1;
        }
    }
}
