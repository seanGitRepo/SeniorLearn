using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using SeniorLearnDataSeed.Data;
using SeniorLearnDataSeed.Data.Core;
using SeniorLearnWebApi.Data;
using SeniorLearnWebApi.Models;


namespace SeniorLearnWebApi.Controllers
{
    [Route("api/blogs")]
    [ApiController]
    public class BlogController : Controller
    {

        private readonly BlogRepository _repo;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BlogController(IConfiguration config, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _repo = new BlogRepository(config);
            _context = context;
            _userManager = userManager;
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
                var userID = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                var user = _context.Users
                    .OfType<ApplicationUser>()
                    .FirstOrDefault(u => u.Id == userID);
                //var user1 =  _userManager.FindByNameAsync(model.Username);
                string fName = user.FirstName;
                string lName = user.LastName;
                string fullName = fName + " " + lName;
                blog.ApplicationUserId = userID;
                blog.CreatorName = fullName;
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
