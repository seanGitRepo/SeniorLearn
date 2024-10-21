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
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;


//TODO: there is no security when trying to access pages.

namespace SeniorLearnDataSeed.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CourseController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CourseController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        //GET :course/mycourses

        

        public async Task<IActionResult> StandardMemberIndex(string searchString, string[] difficulties)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userID = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                var courses = await _context.Courses
                    .Where(c => c.ApplicationUserId != userID)
                    .Include(c=> c.Sessions)
                    .ToListAsync();

                var cd = new List<Details>();

                if (difficulties != null && difficulties.Length > 0)
                {
                    courses = courses.Where(c => difficulties.Contains(c.Difficulty)).ToList();
                }

                foreach (var item in courses)
                {
                    cd.Add(new Models.Course.Details(item));
                }

                if (!string.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToUpper();
                    cd = cd.Where(c => c.Category.ToUpper().Contains(searchString)).ToList();
                    return View(cd);
                }
                if (cd.IsNullOrEmpty())
                {
                    return View("Empty");
                }
                else
                {
                    return View(cd);
                }
            }
            else
            {
                return RedirectToAction("HomeScreen", "Home");
            }
        }

        public async Task<IActionResult> MyCourses(string[] difficulties)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userID = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                var proMemberCourses = await _context.Courses
                    .Where(c => c.ApplicationUserId == userID)
                    .ToListAsync();
                
                if(difficulties != null && difficulties.Length > 0)
                {
                    proMemberCourses = proMemberCourses.Where(c => difficulties.Contains(c.Difficulty)).ToList();
                }

                var cd = new List<Details>();
                foreach (var item in proMemberCourses)
                {
                    cd.Add(new Models.Course.Details(item));
                }

                if (cd.IsNullOrEmpty())
                {
                    //return Forbid();
                    return View("Empty");
                }
                else
                {
                    return View(cd);
                }

                
            }
            
            else
            {
                return RedirectToAction("HomeScreen", "Home");
            }

            
            
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

                var model = new Create
                {
                    CourseDifficulty = new List<SelectListItem> {
                        new SelectListItem { Value = Difficulty.AllLevels, Text = "All Levels"},
                        new SelectListItem { Value = Difficulty.Beginner, Text = "Beginner"},
                        new SelectListItem { Value = Difficulty.Intermediate, Text = "Intermediate"},
                        new SelectListItem { Value = Difficulty.Advanced, Text = "Advanced"}
                }
                };
                return View(model);
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
                var user = await _context.Users
                    .OfType<ApplicationUser>()
                    .FirstOrDefaultAsync(u => u.Id == userID);
                string firstName = user.FirstName.ToUpper();
                string lastName = user.LastName.ToUpper();
                string creatorName = firstName + " " + lastName;

                if (userRole == "Admin" || userRole == "Honourary" || userRole == "Pro")
                {
                    m.ApplicationUserId = userID;
                   
                        try
                        {

                        var course = new Course
                        {
                            Name = m.Name,
                            Description = m.Description,
                            Category = m.Category,
                            Difficulty = m.SelectedDifficulty,
                            CreatorName = creatorName,
                            ApplicationUserId = m.ApplicationUserId,
                            isStandAlone = m.isStandAlone
                         };

                            _context.Add(course);
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Create", "Session", new {courseId = course.CourseId});
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
                    Category = course.Category,
                    SelectedDifficulty = course.Difficulty,
                    CourseDifficulty = new List<SelectListItem> {
                       new SelectListItem { Value = Difficulty.AllLevels, Text = "All Levels" },
                       new SelectListItem { Value = Difficulty.Beginner, Text = "Beginner" },
                       new SelectListItem { Value = Difficulty.Intermediate, Text = "Intermediate" },
                       new SelectListItem { Value = Difficulty.Advanced, Text = "Advanced" }
                    },
                    //ApplicationUserId = course.ApplicationUserId,
                    isStandAlone = course.isStandAlone
                };
                c.CourseDifficulty.FirstOrDefault(x => x.Value == course.Difficulty).Selected = true;
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
                course.Category = model.Category;
                course.Difficulty = model.SelectedDifficulty;
                //course.ApplicationUserId = model.ApplicationUserId; // not sure if this should be removed
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

                var roles = _roleManager.Roles.ToList();

               


                if (course == null)
                {
                    return NotFound();
                }
                var m = new Details(course);


                var sessions = await SessionDetailsList(m.CourseId);

                m.Sessions = sessions;

                var courseCreator = await _context.Users.OfType<ApplicationUser>()
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
        public async Task<IActionResult> EnrollDetailsContinuous(int? id)
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

                var courseCreator = await _context.Users.OfType<ApplicationUser>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Id == course.ApplicationUserId);


                m.MemberName = $"{courseCreator.FirstName} {courseCreator.LastName}";

                ViewData["Events"] = JSONListHelper.GetEventListJsonString(m.Sessions); //Gives the JSON helper the list of Sessions and puts it in a class that is accepted by the FullCalender File.


                
                    return View("EnrollDetailsContinuous", m);
                
                  

            }
            else
            {
                return RedirectToAction("HomeScreen", "Home");
            }



        }
        public async Task<IActionResult> EnrollDetails(int? id)
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

                var courseCreator = await _context.Users.OfType<ApplicationUser>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Id == course.ApplicationUserId);


                m.MemberName = $"{courseCreator.FirstName} {courseCreator.LastName}";

                ViewData["Events"] = JSONListHelper.GetEventListJsonString(m.Sessions); //Gives the JSON helper the list of Sessions and puts it in a class that is accepted by the FullCalender File.


                
                    return View("EnrollDetails", m);
                
                
            }
            else
            {
                return RedirectToAction("HomeScreen", "Home");
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
