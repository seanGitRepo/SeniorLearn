using Microsoft.AspNetCore.Mvc;
using SeniorLearnDataSeed.Data.Core;
using SeniorLearnDataSeed.Models.Enrollments;
using SeniorLearnDataSeed.Data;
using Microsoft.EntityFrameworkCore;
using SeniorLearnDataSeed.Helpers;

namespace SeniorLearnDataSeed.Controllers
{
    public class EnrollmentController : Controller

    {
        private readonly ApplicationDbContext _context;

        public EnrollmentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EnrollContinuousConfirmation(int courseId, int sessionId)
        {
            var course = _context.Courses.FirstOrDefault(x => x.CourseId == courseId);
            var session = _context.Sessions.FirstOrDefault(s=>s.SessionId == sessionId);

            var model = new EnrollmentConfirmationViewModel
            {
                CourseId = course.CourseId,
                CourseName = course.Name,
                CourseDescription = course.Description,
                SessionId = session.SessionId,
                SessionStartTime = session.StartTime,
                SessionEndTime = session.EndTime
            };
            return View("~/Views/Enrollment/EnrollContinuousConfirmation.cshtml", model);
        }
            
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnrollContinuous(EnrollmentRepository enrollmentRepository, int courseId, int sessionId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
                if (userRole == "Honourary" || userRole == "Standard" || userRole == "Pro")
                {

                    var userID = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                    enrollmentRepository.ApplicationUserId = userID;
                    var course = await _context.Courses
                        .Include(c => c.Sessions)
                        .FirstOrDefaultAsync(c => c.CourseId == courseId);
                    if (course == null)
                    {
                        return NotFound();
                    }

                    var session = await _context.Sessions
                        .Where(c => c.CourseId == course.CourseId)
                        .FirstOrDefaultAsync(s => s.SessionId == sessionId);
                    if (session == null)
                    {
                        return NotFound();
                    }
                    var existingEnrollment = _context.Enrollments
                        .FirstOrDefault(e => e.SessionId == session.SessionId && e.ApplicationUserId == enrollmentRepository.ApplicationUserId);
                    if (existingEnrollment != null)
                    {
                        TempData["error"] = "You are already enrolled in this session";

                        return RedirectToAction("EnrollDetailsContinuous", "Course", new { id = courseId });
                    }

                    var user = await _context.Users.OfType<ApplicationUser>()
                        .Include(u => u.Enrollments)
                        .FirstOrDefaultAsync(u => u.Id == userID);

                    var enrollment = new Enrollment
                    {
                        SessionId = session.SessionId,
                        ApplicationUserId = enrollmentRepository.ApplicationUserId
                    };
                    _context.Enrollments.Add(enrollment);
                    user.Enrollments.Add(enrollment);
                    session.EnrolledMembers.Add(enrollment);

                    await _context.SaveChangesAsync();
                    TempData["success"] = "Enrolled successfully";
                    return RedirectToAction("EnrollDetailsContinuous", "Course", new { id = courseId });


                }
                else
                {
                    TempData["error"] = "Must be a member";
                    return Forbid();

                }







            }
            else
            {
                TempData["error"] = "Must be logged in";
                return RedirectToAction("Login", "Account");
            }
        }
    

        [HttpPost]
        public async Task<IActionResult> Enroll(EnrollmentRepository enrollmentRepository, int courseId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
                if (userRole == "Honourary" || userRole == "Standard" || userRole == "Pro")
                {
                    
                    var userID = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                    enrollmentRepository.ApplicationUserId = userID;
                    var course = await _context.Courses
                        .Include(c => c.Sessions)
                        .FirstOrDefaultAsync(c => c.CourseId == courseId);
                    if (course == null)
                    {
                        return NotFound();
                    }
                    foreach (var s in course.Sessions)
                    {
                     var existingEnrollment = _context.Enrollments
                        .FirstOrDefault(e => e.SessionId == s.SessionId && e.ApplicationUserId == enrollmentRepository.ApplicationUserId);
                        if(existingEnrollment != null)
                        {
                            TempData["error"] = "You are already enrolled in this session";

                            return RedirectToAction("EnrollDetails", "Course", new {id = courseId});
                        }
                        
                    }
                
                
                    var user = await _context.Users.OfType<ApplicationUser>()
                        .Include(u => u.Enrollments)
                        .FirstOrDefaultAsync(u => u.Id == userID);

                    foreach (var session in course.Sessions)
                    {
                        var enrollment = new Enrollment
                        {
                            SessionId = session.SessionId,
                            ApplicationUserId = enrollmentRepository.ApplicationUserId

                        };
                        _context.Enrollments.Add(enrollment);

                        user.Enrollments.Add(enrollment);
                        session.EnrolledMembers.Add(enrollment);

                    }
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Enrolled successfully";
                    return RedirectToAction("EnrollDetails", "Course", new { id = courseId });


                }
                else
                {
                    TempData["error"] = "Must be a member";
                    return Forbid();
                    
                }







            }
            else
            {
                TempData["error"] = "Must be logged in";
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
    

