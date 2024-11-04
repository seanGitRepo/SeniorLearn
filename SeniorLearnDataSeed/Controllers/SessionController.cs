
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SeniorLearnDataSeed.Data;
using SeniorLearnDataSeed.Data.Core;
using SeniorLearnDataSeed.Models.Course;
using SeniorLearnDataSeed.Models.Session;
using SeniorLearnDataSeed.Models.Course;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SeniorLearnDataSeed.Controllers
{
    [Route("session")]
    public class SessionController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<CourseController> _logger;

        public SessionController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index() => View(await _context.Sessions.Select(p => new Models.Session.SessionDetails(p)).ToListAsync());// this will show every session in the databas.


        [HttpGet]
        [Route("/Session/Create/{CourseId}", Name = "CreaterSesh")]
        public IActionResult Create(int courseId)
        {
            var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            if (userRole == "Admin" || userRole == "Honourary" || userRole == "Pro") //TODO: check if this would be sufficent security (note, i dont think it would be enough security, but it would be a nice way to check if it was a correct user.
            {


                var model = new SessionCreate
                {
                    CourseId = courseId

                };

                var statuslist = new List<string>
                {
                    "Cancelled","Scheduled", "Draft", "Complete", "Closed"
                };



                // Pass the list to the view via ViewBag or ViewModel
                ViewBag.StatusOptions = new SelectList(statuslist);

                return View(model);


            }

            return Forbid();
        }

        public IActionResult Confirmation(string courseName)
        {
            ViewData["CourseName"] = courseName;
            return View();
        }

        [HttpPost]
        [Route("/Session/Create/{CourseId}")]
        public async Task<IActionResult> Create(SessionCreate m)
        {

            var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            if (userRole == "Admin" || userRole == "Honourary" || userRole == "Pro") 
            {


                if (ModelState.IsValid)
                {
                    Session session;


                    // Check session type (discriminator)
                    if (m.StreetName != null)
                    {
                        session = new OnPremSession
                        {

                            CourseId = m.CourseId, // Set CourseId for the session
                            StartTime = m.StartTime,
                            EndTime = m.EndTime,
                            Status = (SessionStatus)Enum.Parse(typeof(SessionStatus), m.SelectedStatus),
                            StreetNumber = m.StreetNumber, // OnPrem specific property
                            StreetName = m.StreetName,
                            Suburb = m.Suburb


                        };
                    }
                    else if (m.MeetingLink != null)
                    {
                        session = new OnlineSession
                        {
                            CourseId = m.CourseId, // Set CourseId for the session
                            StartTime = m.StartTime,
                            EndTime = m.EndTime,
                            Status = (SessionStatus)Enum.Parse(typeof(SessionStatus), m.SelectedStatus),
                            OnlineLink = m.MeetingLink // OnPrem specific property
                        };
                    }
                    else
                    {
                        Console.WriteLine("failed");
                        return BadRequest("Invalid session type");
                    }




                    _context.Sessions.Add(session);
                    await _context.SaveChangesAsync();
                    //TODO:Redirect this to the same course page.
                    return View("~/Views/Session/Confirmation.cshtml");
                }

            }
            return Forbid();
        }

        [HttpGet]
        [Route("/Session/Edit/{SessionId}", Name = "EditSesh")]
        public async Task<IActionResult> Edit(int SessionId)
        {
            var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            if (userRole == "Admin" || userRole == "Honourary" || userRole == "Pro")
            {

                var m = await _context.Sessions.FindAsync(SessionId);

                var onPremSessions = await _context.Sessions.OfType<OnPremSession>().ToListAsync();
                var onlineSessions = await _context.Sessions.OfType<OnlineSession>().ToListAsync();

                var statuslist = new List<string>
                      {
                          "Cancelled","Scheduled", "Draft", "Complete", "Closed"
                      };

                // Pass the list to the view via ViewBag or ViewModel
                ViewBag.StatusOptions = new SelectList(statuslist);

                var toEdit = new SessionEdit();

                foreach (var session in onPremSessions)
                {

                    if (session.SessionId == SessionId)
                    {

                        toEdit = new SessionEdit()
                        {
                            SessionId = m.SessionId,
                            CourseId = m.CourseId,
                            session_type = "session_onprem"
                        };
                    }
                }

                //TODO: what happens when a customer changes an entity from onprem to online and vise versa


                foreach (var session in onlineSessions)
                {
                    if (session.SessionId == SessionId)
                    {

                        toEdit = new SessionEdit()
                        {
                            SessionId = m.SessionId,
                            CourseId = m.CourseId,
                            session_type = "session_online"
                        };
                    }
                }



                return View(toEdit);
            }
            return Forbid();
        }




        [HttpPost]
        [Route("/Session/Edit/{SessionId}")]
        public async Task<IActionResult> Edit(SessionEdit m)
        {

            var c = await _context.Courses.FindAsync(m.CourseId);
            
            var userID = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            if (userRole == "Admin" || userRole == "Honourary" || userRole == "Pro" && userID == c.ApplicationUserId)
            {

                if (m.session_type == "session_onprem")
                {
                    var session = await _context.Sessions
                        .OfType<OnPremSession>()
                        .FirstOrDefaultAsync(c => c.SessionId == m.SessionId);



                    if (session == null)
                    {
                        return NotFound();
                    }

                    session.StartTime = m.StartTime;
                    session.EndTime = m.EndTime;
                    session.Status = (SessionStatus)Enum.Parse(typeof(SessionStatus), m.SelectedStatus);
                    session.StreetNumber = m.StreetNumber; // OnPrem specific property
                    session.StreetName = m.StreetName; // OnPrem specific property
                    session.Suburb = m.Suburb;


                    await _context.SaveChangesAsync();

                }
                else if (m.session_type == "session_online")
                {

                    var session = await _context.Sessions
                     .OfType<OnlineSession>()
                     .FirstOrDefaultAsync(c => c.SessionId == m.SessionId);



                    if (session == null)
                    {
                        return NotFound();
                    }

                    session.StartTime = m.StartTime;
                    session.EndTime = m.EndTime;
                    session.Status = (SessionStatus)Enum.Parse(typeof(SessionStatus), m.SelectedStatus);
                    session.MeetingCode = m.MeetingLink;


                    await _context.SaveChangesAsync();

                }
                else
                {
                    Console.WriteLine("failed");
                    return BadRequest("Invalid session type");
                }





                //TODO: get this to link back to course page.
                return RedirectToAction("Details", "Course", m.SessionId);

            }
            return Forbid();
           
        }

        [HttpGet]
        [Route("/Session/Delete/{SessionId}", Name = "DeleteSesh")]
        public async Task<IActionResult> Delete(int SessionId)
        {
            var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            if (userRole == "Admin" || userRole == "Honourary" || userRole == "Pro")
            {

                var sesh = await _context.Sessions.FindAsync(SessionId); //returns the session with the requested Id.



                if (sesh == null)
                {
                    return NotFound();
                }

                var m = new SessionDetails
                {
                    CourseId = sesh.CourseId,
                    SessionId = sesh.SessionId,
                    StartTime = sesh.StartTime,
                    EndTime = sesh.EndTime,
                    status = (SessionStatusModel)sesh.Status
                };
              

                return View(m); //passing the sessiondetails to the delte screen to display which session to delete.
                                //then the post will delete the _contesxt sessions when the user confirsm it on the delte screen.
            }
            return Forbid(); 
        }

        // POST: Course/Delete/5
        [HttpPost]
        [Route("/Session/Delete/{SessionId}")]
        public async Task<IActionResult> DeleteConfirmed(int SessionId)
        {
            var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            var UserID =User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var sesh = await _context.Sessions.FindAsync(SessionId);
            var course = await _context.Courses.FindAsync(sesh.CourseId);
           
            


            if (userRole == "Admin" || userRole == "Honourary" || userRole == "Pro" && UserID == course.ApplicationUserId) // this also checks if the current user is the creator of the course.
            {
                

                int? id = sesh.CourseId;

                if (sesh != null)
                {
                    _context.Sessions.Remove(sesh);
                    await _context.SaveChangesAsync();
                }
                //TODO: get this to redirect back to the course page.
                return RedirectToAction("Index", "Course");
            }
            return Forbid();
        }
        


    }
}
