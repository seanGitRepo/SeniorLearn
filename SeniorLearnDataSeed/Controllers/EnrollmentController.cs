using Microsoft.AspNetCore.Mvc;

namespace SeniorLearnDataSeed.Controllers
{
    public class EnrollmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
