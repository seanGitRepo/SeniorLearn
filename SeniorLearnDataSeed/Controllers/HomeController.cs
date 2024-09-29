
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeniorLearnDataSeed.Data;
using SeniorLearnDataSeed.Data.Core;
using SeniorLearnDataSeed.Helpers;
using SeniorLearnDataSeed.Models;
using SeniorLearnDataSeed.Models.Session;
using SQLitePCL;
using System.Collections.Immutable;
using System.Diagnostics;

namespace SeniorLearnDataSeed.Controllers
{
    //if a user tries to access the index method then they will need to be authorised

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
            List<SessionDetails> ListToSend = new List<SessionDetails>();

            var c = await _context.Courses.ToListAsync();

            foreach (var item in c)
            {
                var listCourseSesisoins = await SessionDetailsList(item.CourseId);


                foreach (var sesh in listCourseSesisoins)
                {
                    ListToSend.Add(sesh);
                }
               
                
            }

            var user = User.Identity.Name;

            ViewBag.UserName = user;

            ViewData["Events"] = JSONListHelper.GetEventListJsonString(ListToSend);
            return View(0);
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

