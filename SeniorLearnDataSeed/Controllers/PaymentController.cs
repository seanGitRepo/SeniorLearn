using Microsoft.AspNetCore.Mvc;
using SeniorLearnDataSeed.Data;
using SeniorLearnDataSeed.Data.Core;
using SeniorLearnDataSeed.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SeniorLearnDataSeed.Models.Payment;
using Microsoft.AspNetCore.Authorization;

namespace SeniorLearnDataSeed.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {

        private readonly ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> SignUp()
        {
            return View("SignUp");
        }
        
        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminIndex()
        {

           var objPaymentList = _context.Payments
                .Include(c => c.User)
                .ToList();

            List<PaymentRepository> expriedRegistration = new List<PaymentRepository>();
            List<PaymentRepository> inDateRegistration = new List<PaymentRepository>();
            foreach (var item in objPaymentList)
            {

                var dateDiff = item.userRegistrationDate - DateTime.Now;

                if (dateDiff.Days >= -365) // if true then this is an ok date diff
                {

                    PaymentRepository inDate = new PaymentRepository(item);

                    inDateRegistration.Add(inDate);
                }
                else
                {
                    PaymentRepository expired = new PaymentRepository(item);

                    expriedRegistration.Add(expired);
                }

            }






            AdminIndexView x = new AdminIndexView
            {
                expriedRegistration = expriedRegistration,
                inDateRegistration = inDateRegistration
            };


            return View("PaymentAdmin", x);

        }
        [Authorize(Roles = "Admin")]
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

        [Authorize]
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
        [Authorize(Roles = "Admin")]
        public IActionResult CreateAdmin()
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
                return View("CreateAdmin", model);
            }
            else
            {
                return RedirectToAction("HomeScreen", "Home");
            }

        }

        [HttpPost]
          [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAdmin(PaymentRepository model)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userID = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                var user = await _context.Users.OfType<ApplicationUser>()
                           .FirstOrDefaultAsync(u => u.Id == userID);
                var payment = new Payment
                {

                    PaymentType = model.SelectedPaymentType,
                    AmountPaid = model.AmountPaid,
                    ApplicationUserId = userID,
                    userRegistrationDate = DateTime.Now.AddDays(-DateTime.Today.Day).AddMonths(1),
                };

                if (model.SelectedPaymentType == PaymentType.EFT || model.SelectedPaymentType == PaymentType.CreditCard)
                {
                    payment.CardNumber = model.CardNumber;
                    payment.CardExpiry = model.CardExpiry;
                    payment.CVV = model.CVV;
                }

                _context.Payments.Add(payment);
                user.Payments.Add(payment);
                await _context.SaveChangesAsync();



                TempData["success"] = "subscribed successfully";
                return Redirect("AdminIndex");
            }
            else
            {
                return RedirectToAction("HomeScreen", "Home");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(PaymentRepository model)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userID = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                var user =  await _context.Users.OfType<ApplicationUser>()
                           .FirstOrDefaultAsync(u => u.Id == userID);
                var payment = new Payment
                {

                    PaymentType = model.SelectedPaymentType,
                    AmountPaid = model.AmountPaid,
                    ApplicationUserId = userID,
                    userRegistrationDate = DateTime.Now.AddDays(-DateTime.Today.Day).AddMonths(1),
                };

                if(model.SelectedPaymentType == PaymentType.EFT || model.SelectedPaymentType == PaymentType.CreditCard)
                {
                    payment.CardNumber = model.CardNumber;
                    payment.CardExpiry = model.CardExpiry;
                    payment.CVV = model.CVV;
                }

                _context.Payments.Add(payment);
                user.Payments.Add(payment);
                await _context.SaveChangesAsync();
                

                
                TempData["success"] = "Payment created successfully";
                return RedirectToAction("HomeScreen", "Home");
            }
            else
            {
                return RedirectToAction("HomeScreen", "Home");
            }
            
        }
        [Authorize(Roles = "Admin")]
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
                    CardNumber = paymentFromDb.CardNumber,
                    CardExpiry = paymentFromDb.CardExpiry,
                    CVV = paymentFromDb.CVV,
                    PaymentStatuses = Enum.GetValues(typeof(PaymentStatus))
                    .Cast<PaymentStatus>()
                    .Select(pt => new SelectListItem
                    {
                        Value = pt.ToString(),
                        Text = pt.ToString(),
                        Selected = pt == paymentFromDb.PaymentStatus
                    }).ToList(),
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? PaymentId, PaymentRepository model)
        {
            try
            {
                var userID = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                var user = await _context.Users.OfType<ApplicationUser>()
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
                payment.PaymentStatus = model.SelectedPaymentStatus;
                payment.AmountPaid = model.AmountPaid;
                if(model.SelectedPaymentType == PaymentType.EFT || model.SelectedPaymentType == PaymentType.CreditCard)
                {
                    payment.CardNumber = model.CardNumber;
                    payment.CardExpiry = model.CardExpiry;
                    payment.CVV = model.CVV;
                }

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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult>DeletePOST(int? PaymentId, PaymentRepository model)
        {
            try
            {
                var userID = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                var user = await _context.Users.OfType<ApplicationUser>()
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

