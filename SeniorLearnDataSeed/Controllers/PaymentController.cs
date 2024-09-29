using Microsoft.AspNetCore.Mvc;

namespace SeniorLearnDataSeed.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {


            return View();
        }

        //I was thinking that before the customer can access the homepage, there needs to be a check before accessing any page.



    }
}
