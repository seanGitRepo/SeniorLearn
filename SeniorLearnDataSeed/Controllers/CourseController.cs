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
        public async Task<IActionResult> Index() => View(await _context.Courses.Select(p => new Models.Course.Details(p)).ToListAsync()); //returns all courses

        



        // GET: Course/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        [HttpPost]
        public async Task<IActionResult> Create(Create m)
        {
            if (ModelState.IsValid)
            {
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
                }catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);//for the developer
                    ModelState.AddModelError("", "Oopst daisy, bummer try again");//generic for the user
                }
            }

            return View(m);
        }


        // GET: Course/Edit/5
        public async Task<IActionResult> Edit(int id)
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

        // POST: Course/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(Edit model)
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

        // GET: Course/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
          

            var course = await _context.Courses.FirstOrDefaultAsync(m => m.CourseId == id);

            if (course == null)
            {
                return NotFound();
            }

            var c = new Models.Course.Details(course);

            return View(c);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(m => m.CourseId == id);
           
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }



        // GET: Course/Details/5
        
        public async Task<IActionResult> Details(int? id)
        {


                var course = await _context.Courses
                  .Include(c => c.Sessions)
                  .FirstOrDefaultAsync(m => m.CourseId == id);



           
            if (course == null)
            {
                return NotFound();
            }
            var m = new Details(course);


            m.Sessions = await SessoinReturn(course.CourseId);


            var courseCreator = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == course.ApplicationUserId); 


            m.MemberName = $"{courseCreator.FirstName} {courseCreator.LastName}"; 

            ViewData["Events"] = JSONListHelper.GetEventListJsonString(m.Sessions); //Gives the JSON helper the list of Sessions and puts it in a class that is accepted by the FullCalender File.

           


            return View(m);
        }

      
        //TODO:spelling error.
        public async Task<List<SessionDetails>>SessoinReturn(int courseId) // Returns a course list of sessions, with location.
        {
            List<SessionDetails> sessions = new List<SessionDetails>();
            

            var onPremSessions =  _context.Sessions.OfType<OnPremSession>().ToListAsync();
            var onlineSessions = _context.Sessions.OfType<OnlineSession>().ToListAsync();

            var toDisplay = new SessionDetails();

            foreach (var session in await onPremSessions)
            {

                if (session.CourseId == courseId)
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


                    sessions.Add(toDisplay);
                }
            }


            foreach (var session in await onlineSessions)
            {
                if (session.CourseId == courseId)
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

                    sessions.Add(toDisplay);
                }
            }






            return sessions;
        }
       

    }
}
