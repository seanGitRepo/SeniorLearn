
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeniorLearnDataSeed.Data;
using SeniorLearnDataSeed.Data.Core;
using SeniorLearnDataSeed.Helpers;
using SeniorLearnDataSeed.Models;
using SeniorLearnDataSeed.Models.Session;


using System.Diagnostics;

namespace SeniorLearnDataSeed.Controllers
{
    //if a user tries to access the index method then they will need to be authorised
    //everyone at the momen has the ability to access this screen.
   // [Area("Admin")]
    //[Authorize(Roles = RoleDetail.Role_Admin)]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        

        //TODO: Home page is a calender of all the courses the current user is doing.
        public HomeController( ApplicationDbContext context, ILogger<HomeController> logger)
        {
          
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {

         
            //TODO: Check if a payment has been sucessful, or a way to manage all



            return RedirectToAction("HomeScreen", "Home");
        }


        public async Task<IActionResult> HomeScreen()
        {

            if (User.Identity.IsAuthenticated)
            {
                return Redirect("HomeScreenLogged");
            }

            List<SessionDetails> ListToSend = new List<SessionDetails>();

            var c = await _context.Courses.ToListAsync();

            foreach (var item in c)
            {
                var listCourseSesisoins = await SessionDetailsList(item.CourseId);
                

                foreach (var sesh in listCourseSesisoins)
                {
                        

                    ListToSend.Add(sesh); // this is not passing the organiser ID which means that a hacker wouldnt be able to get the info on the course ID or role.
                }
               
                
            }

            var user = User.Identity.Name;

            ViewBag.UserName = user;

            ViewData["Events"] = JSONListHelper.GetEventListJsonString(ListToSend);

            return View();
        }
        [Authorize]
        public async Task<IActionResult> HomeScreenLogged()
        {
            List<SessionDetails> ListToSend = new List<SessionDetails>();

            var courses = await _context.Courses.ToListAsync();

            foreach (var item in courses)
            {
                var listCourseSessions = await SessionDetailsList(item.CourseId);

                foreach (var session in listCourseSessions)
                {
                    ListToSend.Add(session);
                }
            }

            var user = User.Identity.Name;
            ViewBag.UserName = user;
            ViewData["Events"] = JSONListHelper.GetEventListJsonString(ListToSend);

            // Retrieve and pass distinct categories for the dropdown
            ViewBag.Categories = await _context.Courses
                .Select(c => c.Category)
                .Distinct()
                .ToListAsync();




            return View();
        }






        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<List<SessionDetails>> SessionDetailsList(int CourseID)
        {


            List<SessionDetails> sessions = new List<SessionDetails>();

            var course = await _context.Courses.FindAsync(CourseID);

            var onPremSessions = _context.Sessions
                .AsNoTracking()
                .OfType<OnPremSession>();

            var onlineSessions = _context.Sessions
                .AsNoTracking()
                .OfType<OnlineSession>();

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

