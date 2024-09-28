
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SeniorLearnDataSeed.Data;
using SeniorLearnDataSeed.Data.Core;
using SeniorLearnDataSeed.Models.Course;
using SeniorLearnDataSeed.Models.Session;
using SeniorLearnDataSeed.Models.Course;

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
            var model = new SessionCreate
            {
                CourseId = courseId

            };

            

            return View(model);
        }

        [HttpPost]
        [Route("/Session/Create/{CourseId}")]
        public async Task<IActionResult> Create(SessionCreate m)
        {

          


            if (ModelState.IsValid)
            {
                Session session;

               
                // Check session type (discriminator)
                if (m.session_type == "session_onprem")
                {
                    session = new OnPremSession
                    {
                        CourseId = m.CourseId, // Set CourseId for the session
                        StartTime = m.StartTime,
                        EndTime = m.EndTime,
                        Status = (SessionStatus)m.status,
                        StreetNumber = m.location // OnPrem specific property
                    };
                }
                else if (m.session_type == "session_online")
                {
                    session = new OnlineSession
                    {
                        CourseId = m.CourseId, // Set CourseId for the session
                        StartTime = m.StartTime,
                        EndTime = m.EndTime,
                        Status = (SessionStatus)m.status,
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

                return RedirectToAction("Index","Course");
            }

            return View(m);
        }

        [HttpGet]
        [Route("/Session/Edit/{SessionId}", Name = "EditSesh")]
        public async Task<IActionResult> Edit(int SessionId)
        {
            var m = await _context.Sessions.FindAsync(SessionId);

            var onPremSessions = await _context.Sessions.OfType<OnPremSession>().ToListAsync();
            var onlineSessions = await _context.Sessions.OfType<OnlineSession>().ToListAsync();

           

            var toEdit = new SessionEdit();

            foreach (var session in onPremSessions)
            {

                if (session.SessionId == SessionId)
                {

                    toEdit = new SessionEdit()
                    {
                        SessionId = m.SessionId,
                        CourseId = m.CourseId,
                        session_type = "session_onprem" //TODO: we would have the automation variable in here for the type of session with the drop down menu.
                    };
                }
            }


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




        [HttpPost]
        [Route("/Session/Edit/{SessionId}")]
        public async Task<IActionResult> Edit(SessionEdit m)
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
                session.Status = (SessionStatus)m.status;
                session.StreetNumber = m.location; // OnPrem specific property

               
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
                session.Status = (SessionStatus)m.status;
                session.MeetingCode = m.location;

               
                await _context.SaveChangesAsync();

            }
            else
            {
                Console.WriteLine("failed");
                return BadRequest("Invalid session type");
            }




            

            return RedirectToAction("Details", "Course", m.SessionId);


           
        }

        [HttpGet]
        [Route("/Session/Delete/{SessionId}", Name = "DeleteSesh")]
        public async Task<IActionResult> Delete(int SessionId)
        {


            var sesh = await _context.Sessions.FindAsync(SessionId); //returns the session with the requested Id.

            

            if (sesh == null)
            {
                return NotFound();
            }

            var m = new SessionDetails
            {
                SessionId = sesh.SessionId,
                StartTime = sesh.StartTime,
                EndTime = sesh.EndTime,
                status = (SessionStatusModel)sesh.Status
            };
           

            return View(m); //passing the sessiondetails to the delte screen to display which session to delete.
            //then the post will delete the _contesxt sessions when the user confirsm it on the delte screen.
        }

        // POST: Course/Delete/5
        [HttpPost]
        [Route("/Session/Delete/{SessionId}")]
        public async Task<IActionResult> DeleteConfirmed(int SessionId)
        {
            var session = await _context.Sessions.FirstOrDefaultAsync(m => m.SessionId == id);

            if (session != null)
            {
                _context.Sessions.Remove(session);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Details","Course",id);
        }

        public async Task<SessionDetails> SessoinReturn(int sessionId) // Returns a course list of sessions, with location.
        {
            SessionDetails sessions = new SessionDetails();


            var onPremSessions = _context.Sessions.OfType<OnPremSession>().ToListAsync();
            var onlineSessions = _context.Sessions.OfType<OnlineSession>().ToListAsync();

            var toDisplay = new SessionDetails();

            foreach (var session in await onPremSessions)
            {

                if (session.SessionId == sessionId)
                {

                    toDisplay = new SessionDetails() //TODO: add the remaining variables needed for onPrem and Online.
                    {
                        status = (SessionStatusModel)session.Status,
                        SessionId = session.SessionId,
                        CourseId = session.CourseId,
                        eventLocation = session.StreetName,
                        StartTime = session.StartTime,
                        EndTime = session.EndTime,

                    };


                    sessions = toDisplay;
                }
            }


            foreach (var session in await onlineSessions)
            {
                if (session.SessionId == sessionId)
                {

                    toDisplay = new SessionDetails()
                    {
                        status = (SessionStatusModel)session.Status,
                        SessionId = session.SessionId,
                        CourseId = session.CourseId,
                        eventLocation = session.OnlineLink,
                        StartTime = session.StartTime,
                        EndTime = session.EndTime,
                    };

                    sessions = toDisplay;
                }
            }






            return sessions;
        }


    }
}
