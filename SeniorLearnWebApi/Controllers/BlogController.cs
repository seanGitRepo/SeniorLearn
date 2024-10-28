using Microsoft.AspNetCore.Mvc;

using SeniorLearnWebApi.Data;
using SeniorLearnWebApi.Models;

namespace SeniorLearnWebApi.Controllers
{
    //[Route("api/blogs")]
    [ApiController]
    public class BlogController : Controller
    {

        private readonly BlogRepository _repo;

        public BlogController(IConfiguration config)
        {
            _repo = new BlogRepository(config);
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAll();
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public IActionResult GetBlog([FromRoute] int Id)
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
                return CreatedAtAction(nameof(GetBlog), new { BlogId = blog.BlogId }, blog);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete([FromRoute] int Id)
        {
            _repo.Delete(Id);
            return Ok();
        }
    }
}
