using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeniorLearnDataSeed.Data.Core;
using SeniorLearnDataSeed.Data;
using SeniorLearnDataSeed.Models.Course;
using SeniorLearnDataSeed.Models.Session;
using Microsoft.Win32;
using Microsoft.Identity.Client;
using SeniorLearnDataSeed.Helpers;
using SeniorLearnDataSeed.Models.Session;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


//TODO: there is no security when trying to access pages.

namespace SeniorLearnDataSeed.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CourseController> _logger;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Course/Index
        public async Task<IActionResult> Index()
        {

            if (User.Identity.IsAuthenticated)
            {
                var c = await _context.Courses.ToListAsync(); //returns all courses

                var cd = new List<Details>();

                foreach (var item in c)
                {
                    cd.Add(new Details(item));
                }

                return View(cd);
            }

            return RedirectToAction("HomeScreen", "Home");
        }




        // GET: Course/Create
        public IActionResult Create()
        {

            if (User.Identity.IsAuthenticated)
            {
                return View();
            }

           return RedirectToAction("HomeScreen","Home"); 
           
        }

        // POST: Course/Create
        [HttpPost]
        public async Task<IActionResult> Create(Create m)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
                var userID = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                if (userRole == "Admin" || userRole == "Honourary" || userRole == "Pro")
                {
                    m.ApplicationUserId = userID;
                   
                        try
                        {

                            var course = new Course
                            {
                                Name = m.Name,
                                Description = m.Description,
                                ApplicationUserId = m.ApplicationUserId,
                                isStandAlone = m.isStandAlone
                            };

                            _context.Add(course);
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, ex.Message);//for the developer
                            ModelState.AddModelError("", "Oopst daisy, bummer try again");//generic for the user
                        }
                    

                    return Forbid();
                }
            }
            return RedirectToAction("HomeScreen","Home");
        }


        // GET: Course/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (User.Identity.IsAuthenticated)
            {

                var course = await _context.Courses.FindAsync(id);

                if (course == null)
                {
                    return NotFound();
                }

                var c = new Edit
                {
                    CourseId = course.CourseId,
                    Name = course.Name,
                    Description = course.Description,
                    ApplicationUserId = course.ApplicationUserId,
                    isStandAlone = course.isStandAlone
                };
                return View(c);
            }
            return RedirectToAction("HomeScreen", "Home");
        }

        // POST: Course/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(Edit model)
        {
            if (User.Identity.IsAuthenticated)
            {

                var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == model.CourseId);

                if (course == null)
                {
                    return NotFound();
                }

                course.Name = model.Name;
                course.Description = model.Description;
                course.ApplicationUserId = model.ApplicationUserId; // not sure if this should be removed
                course.isStandAlone = model.isStandAlone;


                // Add any additional properties that can be updated in your edit model

                await _context.SaveChangesAsync();


                return RedirectToAction("Index");
            }
            return RedirectToAction("HomeScreen", "Home");
        }

        // GET: Course/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (User.Identity.IsAuthenticated)
            {



                var course = await _context.Courses.FirstOrDefaultAsync(m => m.CourseId == id);

                if (course == null)
                {
                    return NotFound();
                }

                var c = new Models.Course.Details(course);

                return View(c);
            }
            return RedirectToAction("HomeScreen", "Home");
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {


                var course = await _context.Courses.FirstOrDefaultAsync(m => m.CourseId == id);

                if (course != null)
                {
                    _context.Courses.Remove(course);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }

            return RedirectToAction("HomeScreen", "Home");
        }



        // GET: Course/Details/5
        
        public async Task<IActionResult> Details(int? id)
        {
            //TODO:have a button on the details page that selects edit, which brings up all the buttons to make changes, rather than having all the buttons at once.

            if (User.Identity.IsAuthenticated)
            {
                var course = await _context.Courses
                 .AsNoTracking()
                  .Include(c => c.Sessions)
                  .FirstOrDefaultAsync(m => m.CourseId == id);




                if (course == null)
                {
                    return NotFound();
                }
                var m = new Details(course);


                var sessions = await SessionDetailsList(m.CourseId);

                m.Sessions = sessions;

                var courseCreator = await _context.ApplicationUsers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Id == course.ApplicationUserId);


                m.MemberName = $"{courseCreator.FirstName} {courseCreator.LastName}";

                ViewData["Events"] = JSONListHelper.GetEventListJsonString(m.Sessions); //Gives the JSON helper the list of Sessions and puts it in a class that is accepted by the FullCalender File.



                return View(m);
            }
            else
            {
                return RedirectToAction("HomeScreen","Home");
            }


            
        }

        public async Task<List<SessionDetails>> SessionDetailsList(int CourseID)
        {


            List<SessionDetails> sessions = new List<SessionDetails>();


            var onPremSessions = _context.Sessions
                .AsNoTracking()
                .OfType<OnPremSession>();

            var course = await _context.Courses.FindAsync(CourseID);

            var onlineSessions = _context.Sessions.AsNoTracking().OfType<OnlineSession>();

            var toDisplay = new SessionDetails();

            foreach (var session in onPremSessions)
            {

                if (session.CourseId == CourseID)
                {
                    string locationtopost = $"{session.StreetNumber} {session.StreetName} {session.Suburb}";

                    toDisplay = new SessionDetails() 
                    {
                        status = (SessionStatusModel)session.Status,
                        SessionId = session.SessionId,
                        CourseId = session.CourseId,
                        eventLocation = locationtopost,
                        StartTime = session.StartTime,
                        EndTime = session.EndTime,
                        CourseName = course.Name
                    };


                    sessions.Add(toDisplay);
                }
            }


            foreach (var sessiono in onlineSessions)
            {
                if (sessiono.CourseId == CourseID)
                {

                    toDisplay = new SessionDetails()
                    {
                        status = (SessionStatusModel)sessiono.Status,
                        SessionId = sessiono.SessionId,
                        CourseId = sessiono.CourseId,
                        eventLocation = sessiono.OnlineLink,
                        StartTime = sessiono.StartTime,
                        EndTime = sessiono.EndTime,
                        CourseName = course.Name
                    };
                    
                    sessions.Add(toDisplay);
                }
            }



            return sessions;
        }






    }
}
