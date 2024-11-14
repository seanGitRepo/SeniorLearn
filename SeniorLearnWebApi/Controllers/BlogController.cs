using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using SeniorLearnWebApi.Data;
using SeniorLearnWebApi.Models;

namespace SeniorLearnWebApi.Controllers
{
    [Route("api/blogs")]
    [ApiController]
    public class BlogController : Controller
    {

        private readonly BlogRepository _repo;

        public BlogController(IConfiguration config)
        {
            _repo = new BlogRepository(config);
        }
        


        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var result = _repo.GetAll();
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public IActionResult GetBlog([FromRoute] ObjectId Id)
        {
            var blog = _repo.GetById(Id);
            if(blog == null)
            {
                return NotFound();

            }
            else
            {
                return Ok(blog);
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] Blog blog)
        {
            if (ModelState.IsValid)
            {
                _repo.Save(blog);
                return CreatedAtAction(nameof(GetBlog), new { Id = blog.BlogId }, blog);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete([FromRoute] ObjectId Id)
        {
            _repo.Delete(Id);
            return Ok();
        }
    }
}
