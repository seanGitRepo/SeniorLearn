using Microsoft.AspNetCore.Mvc;
using SeniorLearnDataSeed.Data.Core;
using SeniorLearnDataSeed.Models.Enrollments;
using SeniorLearnDataSeed.Data;
using Microsoft.EntityFrameworkCore;
using SeniorLearnDataSeed.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace SeniorLearnDataSeed.Controllers
{
    [Authorize]
    public class EnrollmentController : Controller

    {
        private readonly ApplicationDbContext _context;



        public EnrollmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminEnrollIndex()
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.Session)
                .ThenInclude(s => s.Course) // Including the Course to access IsStandAlone
                .Include(e => e.ApplicationUser)
                .ToListAsync();

            // Map enrollments to `EnrollmentRepository` instances
            var enrollmentRepositories = enrollments.Select(e => new EnrollmentRepository(e)).ToList();

            // Separate standalone and recurring enrollments based on Course's IsStandAlone
            var standaloneEnrollments = enrollmentRepositories.Where(e => e.standalone).ToList();
            var recurringEnrollments = enrollmentRepositories.Where(e => !e.standalone).ToList();

            var viewModel = new AdminEnrollIndexViewModel
            {
                StandaloneEnrollments = standaloneEnrollments,
                RecurringEnrollments = recurringEnrollments
            };

            return View("AdminEnrollIndex",viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
       public async Task<IActionResult> DeleteEnrollment(int id)
        {
            // Find the enrollment by ID
            var enrollment = await _context.Enrollments.FindAsync(id);

            if (enrollment == null)
            {
                TempData["error"] = "Enrollment not found";
                return RedirectToAction(nameof(AdminEnrollIndex));
            }

            try
            {
                // Remove the enrollment from the database
                _context.Enrollments.Remove(enrollment);
                await _context.SaveChangesAsync();

                TempData["success"] = "Enrollment deleted successfully";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error deleting enrollment: " + ex.Message;
            }

            return RedirectToAction(nameof(AdminEnrollIndex));
        }
        [Authorize]
        public async Task<IActionResult> MemberIndex()
        {

            if (User.Identity.IsAuthenticated)
            {

                var user = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                var e = await _context.Enrollments
                   .Where(c => c.ApplicationUserId == user)
                   .Include(e => e.Session)
                   .ThenInclude(s => s.Course) // Including the Course to access IsStandAlone
                   .Include(e => e.ApplicationUser)
                   .ToListAsync();

                // Map enrollments to `EnrollmentRepository` instances
                var enrollmentRepositories = e.Select(m => new EnrollmentRepository(m)).ToList();

                // Separate standalone and recurring enrollments based on Course's IsStandAlone
                var standaloneEnrollments = enrollmentRepositories.Where(e => e.standalone).ToList();
                var recurringEnrollments = enrollmentRepositories.Where(e => !e.standalone).ToList();

                var vm = new AdminEnrollIndexViewModel
                {
                    StandaloneEnrollments = standaloneEnrollments,
                    RecurringEnrollments = recurringEnrollments
                };


                return View("MemberIndex", vm);

        }
            return RedirectToAction("HomeScreen", "Home");
    }

        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
    

