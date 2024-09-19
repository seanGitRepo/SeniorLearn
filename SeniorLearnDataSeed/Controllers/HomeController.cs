using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeniorLearnDataSeed.Data.Core;
using SeniorLearnDataSeed.Models;
using System.Diagnostics;

namespace SeniorLearnDataSeed.Controllers
{
    //if a user tries to access the index method then they will need to be authorised

   // [Area("Admin")]
    //[Authorize(Roles = RoleDetail.Role_Admin)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
