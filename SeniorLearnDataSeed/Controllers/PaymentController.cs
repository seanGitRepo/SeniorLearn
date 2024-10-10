using Microsoft.AspNetCore.Mvc;
using SeniorLearnDataSeed.Data;
using SeniorLearnDataSeed.Data.Core;
using SeniorLearnDataSeed.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
                var model = new PaymentViewModel
                {
                    PaymentTypes = Enum.GetValues(typeof(PaymentType))
                    .Cast<PaymentType>()
                    .Select(pt => new SelectListItem
                    {
                        Value = pt.ToString(),
                        Text = pt.ToString()
                    }).ToList()
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("HomeScreen", "Home");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Create(PaymentViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userID = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                var user =  await _context.ApplicationUsers
                           .FirstOrDefaultAsync(u => u.Id == userID);
                var payment = new Payment
                {

                    PaymentType = model.SelectedPaymentType,
                    AmountPaid = model.AmountPaid,
                    ApplicationUserId = userID,
                    userRegistrationDate = DateTime.UtcNow,
                };

                _context.Payments.Add(payment);
                user.Payments.Add(payment);
                _context.SaveChangesAsync();
                TempData["success"] = "Payment created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("HomeScreen", "Home");
            }
            
        }

    }
}
