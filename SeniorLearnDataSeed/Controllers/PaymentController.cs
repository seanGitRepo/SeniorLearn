using Microsoft.AspNetCore.Mvc;
using SeniorLearnDataSeed.Data;

namespace SeniorLearnDataSeed.Controllers
{
    public class PaymentController : Controller
    {

        private readonly ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            var objPaymentList = _context.Payments.ToList();
            return View(objPaymentList);
        }

        //I was thinking that before the customer can access the homepage, there needs to be a check before accessing any page.

        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("HomeScreen", "Home");
        }

    }
}
