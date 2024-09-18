using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeniorLearnDataSeed.Data.Core;
using SeniorLearnDataSeed.Data;
using SeniorLearnDataSeed.Models.Course;

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
        public async Task<IActionResult> Index() => View(await _context.Courses.Select(p => new Models.Course.Details(p)).ToListAsync()); //returns one person

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
                        MemberId = m.MemberId,
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
                MemberId = course.MemberId,
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
                    course.MemberId = model.MemberId; // not sure if this should be removed
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

            var c = new Details
            {
                CourseId = course.CourseId,
                Name = course.Name,
                Description = course.Description,
                MemberId = course.MemberId,
              //  MemberName = $"{course.Member.FirstName} {course.Member.LastName}",
                isStandAlone = course.isStandAlone
            };

            

            return View(c);
        }

      
        

       

    }
}
