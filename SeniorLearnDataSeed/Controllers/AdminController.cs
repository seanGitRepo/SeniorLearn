using Microsoft.AspNetCore.Mvc;
using SeniorLearnDataSeed.Data.Core;
using SeniorLearnDataSeed.Models.Enrollments;
using SeniorLearnDataSeed.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SeniorLearnDataSeed.Models.Admin;
using System.Threading.Tasks;
using System.Linq;
using SeniorLearnDataSeed.Migrations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace SeniorLearnDataSeed.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        
        public AdminController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> adminUserList()
        {
            

            if (User.Identity.IsAuthenticated)
            {
                var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

                if (userRole == "Admin")
                {

                    var users = _userManager.Users.OfType<ApplicationUser>().ToList();

                    var userList = new List<userDetails>();
                    foreach (var user in users)
                    {
                        //ToDo: delete user?.
                        //ToDo: why does the admin role not get assainged when lisitng. check if the admin is changeable.

                        var usert = await _userManager.FindByIdAsync(user.Id);

                        var role = await _userManager.GetRolesAsync(usert);


                        userDetails m = new userDetails
                        {
                            
                            userName = user.UserName,
                            firstName = user.FirstName,
                            lastName = user.LastName,
                            role = role.FirstOrDefault(),
                            userId = user.Id

                        };

                        userList.Add(m);

                    }

                    return View(userList);
                }
                
            }

            return Forbid();


        }


        public IActionResult Index()
        {

            return View();
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> adminListEdit()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

                if (userRole == "Admin")
                {
                    var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();

                    var users = _userManager.Users.ToList();

                    var userList = new List<userDetails>();
                    foreach (var user in users)
                    {

                        var usert = await _userManager.FindByIdAsync(user.Id);

                        var role = await _userManager.GetRolesAsync(usert);


                        userDetails m = new userDetails
                        {
                            userName = user.UserName,
                            firstName = user.FirstName,
                            lastName = user.LastName,
                            role = role.FirstOrDefault(),
                            RoleList = roles,
                            userId = user.Id

                        };


                    

                        userList.Add(m);

                    }

                    return View(userList);
                }

            }

            return Forbid();


        }



        //post 

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminEditSubmit(List<userDetails> userList)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

                if (userRole == "Admin")
                {
                    var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
                    foreach (var userDetails in userList)
                    {
                      
                        var user = await _userManager.FindByIdAsync(userDetails.userId);

                        if (user != null)
                        {

                            if (user.Id == User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value)
                            {// if the user is currently the system admin

                                user.FirstName = userDetails.firstName;
                                user.LastName = userDetails.lastName;

                                await _userManager.UpdateAsync(user);

                            }
                            else
                            {
                                user.FirstName = userDetails.firstName;
                                user.LastName = userDetails.lastName;


                               

                                var currentRoles = await _userManager.GetRolesAsync(user);

                                await _userManager.RemoveFromRolesAsync(user, currentRoles);

                                await _userManager.AddToRoleAsync(user, userDetails.role);

                                await _userManager.UpdateAsync(user);
                            }
                            
                        }
                    }

                    return RedirectToAction("adminUserList");
                }
            }

            return Forbid();
        }
    }
}
