using Microsoft.AspNetCore.Mvc;
using SeniorLearnDataSeed.Data.Core;
using SeniorLearnDataSeed.Models.Enrollments;

namespace SeniorLearnDataSeed.Controllers
{
    public class EnrollmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(EnrollmentRepository enrollmentRepository)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        //        var userID = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        //        if (userRole =="Honourary" || userRole == "Standard" || userRole == "Pro")
        //        {
        //            enrollmentRepository.ApplicationUserId = userID;
        //            try
        //            {
        //                var enrollment = new Enrollment
        //                {
        //                    ApplicationUserId = enrollmentRepository.ApplicationUserId,

        //                }
        //            }
        //        }
        //    }
        //}
    }
}
