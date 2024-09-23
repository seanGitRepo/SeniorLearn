using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeniorLearnDataSeed.Data;

namespace SeniorLearnDataSeed.Controllers
{
    [Route("session")]
    public class SessionController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<CourseController> _logger;

        public SessionController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index() => View(await _context.Sessions.Select(p => new Models.Session.Details(p)).ToListAsync());// this will show every session in the databas.

        [HttpGet("list/{id}")]

        public async Task<IActionResult> CoureSessions([FromQuery]int id)
        {


            return RedirectToAction("session/");
        }





    }
}
