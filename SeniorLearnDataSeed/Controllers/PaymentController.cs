using Microsoft.AspNetCore.Mvc;
using SeniorLearnDataSeed.Data;
using SeniorLearnDataSeed.Data.Core;
using SeniorLearnDataSeed.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SeniorLearnDataSeed.Models.Payment;

namespace SeniorLearnDataSeed.Controllers
{
    public class PaymentController : Controller
    {

        private readonly ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AdminIndex()
        {



            var objPaymentList = _context.Payments.ToList();
            return View("PaymentAdmin", objPaymentList);

        }
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userID = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                var objPaymentList = await _context.Payments
                    .Where(p => p.ApplicationUserId == userID)
                    .ToListAsync();


                return View(objPaymentList);
            }
            else
            {
                return RedirectToAction("HomeScreen", "Home");
            }

            
        }

        //I was thinking that before the customer can access the homepage, there needs to be a check before accessing any page.

        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = new PaymentRepository
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
        public async Task<IActionResult> Create(PaymentRepository model)
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
                await _context.SaveChangesAsync();
                

                
                TempData["success"] = "Payment created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("HomeScreen", "Home");
            }
            
        }

        public IActionResult Edit(int? PaymentId)
        {

            if(PaymentId == null || PaymentId == 0)
            {
                return NotFound();
            }
            if (User.Identity.IsAuthenticated)
            {

                Payment? paymentFromDb = _context.Payments
                    .FirstOrDefault(p => p.PaymentId == PaymentId);

                var model = new PaymentRepository
                {
                    SelectedPaymentType = paymentFromDb.PaymentType,
                    AmountPaid = paymentFromDb.AmountPaid,
                    PaymentTypes = Enum.GetValues(typeof(PaymentType))
                    .Cast<PaymentType>()
                    .Select(pt => new SelectListItem
                    {
                        Value = pt.ToString(),
                        Text = pt.ToString(),
                        Selected = pt == paymentFromDb.PaymentType
                    }).ToList()
                };
                if(paymentFromDb == null)
                {
                    return NotFound();
                }

                
                return View("Edit", model);
            }
            else
            {
                return RedirectToAction("HomeScreen", "Home");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? PaymentId, PaymentRepository model)
        {
            try
            {
                var userID = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                var user = await _context.ApplicationUsers
                           .Include(u => u.Payments)
                           .FirstOrDefaultAsync(u => u.Id == userID);
                if (user == null)
                {
                    return NotFound();
                }



                var payment = await _context.Payments.FirstOrDefaultAsync(p => p.PaymentId == PaymentId);
                if (payment == null)
                {
                    return NotFound();
                }

                
                payment.PaymentType = model.SelectedPaymentType;
                payment.AmountPaid = model.AmountPaid;

                await _context.SaveChangesAsync();
                TempData["success"] = "Payment updated successfully";
                return RedirectToAction("AdminIndex");
            }
            catch
            {
                TempData["error"] = "Did not update successfully";
                return RedirectToAction("AdminIndex");
            } 
                   
            }
        public IActionResult Delete(int? PaymentId)
        {

            if (PaymentId == null || PaymentId == 0)
            {
                return NotFound();
            }
            if (User.Identity.IsAuthenticated)
            {

                Payment? paymentFromDb = _context.Payments
                    .FirstOrDefault(p => p.PaymentId == PaymentId);

                var model = new PaymentRepository
                {
                    SelectedPaymentType = paymentFromDb.PaymentType,
                    AmountPaid = paymentFromDb.AmountPaid,
                    PaymentTypes = Enum.GetValues(typeof(PaymentType))
                    .Cast<PaymentType>()
                    .Select(pt => new SelectListItem
                    {
                        Value = pt.ToString(),
                        Text = pt.ToString(),
                        Selected = pt == paymentFromDb.PaymentType
                    }).ToList()
                };
                if (paymentFromDb == null)
                {
                    return NotFound();
                }


                return View("Delete", model);
            }
            else
            {
                return RedirectToAction("HomeScreen", "Home");
            }

        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult>DeletePOST(int? PaymentId, PaymentRepository model)
        {
            try
            {
                var userID = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                var user = await _context.ApplicationUsers
                           .Include(u => u.Payments)
                           .FirstOrDefaultAsync(u => u.Id == userID);
                
                if (user == null)
                {
                    return NotFound();
                }



                var payment = await _context.Payments.FirstOrDefaultAsync(p => p.PaymentId == PaymentId);
                if (payment == null)
                {
                    return NotFound();
                }

                _context.Payments.Remove(payment);
                
                await _context.SaveChangesAsync();
                TempData["success"] = "Payment deleted successfully";
                return RedirectToAction("AdminIndex");
            }
            catch
            {
                TempData["error"] = "Did not delete successfully";
                return RedirectToAction("AdminIndex");
            }

        }


    }

       

            



    }

