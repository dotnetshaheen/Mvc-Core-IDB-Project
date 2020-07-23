using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mvc_asp_core.Models.UserIdentityModel;
using mvc_asp_core.ViewModel.Identity;

namespace mvc_asp_core.Controllers
{
    public class RoleManageController : Controller
    {
        private UserManager<IdentityModel> userManager;
        private RoleManager<IdentityRole> roleManager;

        public RoleManageController(UserManager<IdentityModel> user, RoleManager<IdentityRole> role)
        {
            userManager = user;
            roleManager = role;
        }
        
        public IActionResult Index()
        {
            List<IdentityRole> role;
            try
            {
                role = roleManager.Roles.ToList();
                if (role.Count()<1)
                {
                    ViewBag.Result = "No Record Is Found";
                }
                return View(role);
            }
            catch (Exception ex)
            {
                ViewBag.Result = ex.Message;
            }
            return View();
        }
        // Create Role
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        // Create Role
        [HttpPost]
        public async Task<IActionResult> Create(RoleVM role)
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name = role.Name
            };

            if (ModelState.IsValid)
            {
                try
                {
                    IdentityResult result = await roleManager.CreateAsync(identityRole);
                    if (result.Succeeded)
                    {
                        TempData["Success"] = "Role Created Successfully.";
                        return RedirectToAction("Index");
                    }
                    if (result.Errors.Count() > 0)
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("/", item.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Result = ex.Message;
                    
                }
            }
            return View();
        }
        // Assign Role
        [HttpGet]
        public IActionResult AssignRole()
        {
            ViewBag.user = new SelectList(userManager.Users, "Id", "UserName");
            ViewBag.roles = new SelectList(roleManager.Roles, "Name","Name");
            return View();
        }
        // Assign Role
        [HttpPost]
        public async Task<IActionResult> AssignRole(string userlst, IEnumerable<string> roles)
        {
            ViewBag.user = new SelectList(userManager.Users, "Id", "UserName");
            ViewBag.roles = new SelectList(roleManager.Roles, "Name", "Name");

            var user = await userManager.FindByIdAsync(userlst);
            IdentityResult result = await userManager.AddToRolesAsync(user, roles);
            if (result.Succeeded)
            {
                ViewBag.Result = user.UserName + " successfully assigned to role " ;
            }
            if (result.Errors.Count() > 0)
            {
                foreach (var s in result.Errors)
                {
                    //ModelState.AddModelError("", s.ToString());
                    ViewBag.message = "Something went wrong";
                    ViewBag.error = s.ToString();
                }

            }

            return View();
        }


    }
}