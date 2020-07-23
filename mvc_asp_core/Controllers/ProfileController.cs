using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mvc_asp_core.Models.UserIdentityModel;

namespace mvc_asp_core.Controllers
{
    public class ProfileController : Controller
    {
        private UserManager<IdentityModel> userManager;
        private readonly IHostingEnvironment hosting;
        public ProfileController(UserManager<IdentityModel> user, IHostingEnvironment _hosting)
        {
            userManager = user;
            hosting = _hosting;
        }
        // Retrive All Profile
        public IActionResult LoadAllProfile()
        {
            List<IdentityModel> user = userManager.Users.ToList();
            return View(user);
        }
        
        // Retrive  Logged User profile
        public async Task<IActionResult> LoggedUserProfile()
        {
            var userId = userManager.GetUserId(HttpContext.User);
            IdentityModel user = await userManager.FindByIdAsync(userId);            
            return View(user);
        }

        // Edit Profile
        [HttpGet]
        public async  Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IdentityModel userModel)
        {
            try
            {
                IdentityModel user = await userManager.FindByIdAsync(userModel.Id);
                if (user != null)
                {
                    user.Id = userModel.Id;
                    //user.AccessFailedCount = userModel.AccessFailedCount;
                    //user.ConcurrencyStamp = userModel.ConcurrencyStamp;
                    user.DateOfBirth = userModel.DateOfBirth;
                    //user.Email = userModel.Email;
                    //user.EmailConfirmed = userModel.EmailConfirmed;
                    user.FirstName = userModel.FirstName;
                    user.LastName = userModel.LastName;
                    //user.imgFile = userModel.imgFile;
                    //user.LockoutEnabled = userModel.LockoutEnabled;
                    //user.LockoutEnd = userModel.LockoutEnd;
                    //user.NameInitial = userModel.NameInitial;
                    //user.NormalizedEmail = userModel.NormalizedEmail;
                    //user.NormalizedUserName = userModel.NormalizedUserName;
                    //user.PasswordHash = userModel.PasswordHash;
                    //user.PhoneNumber = userModel.PhoneNumber;
                    //user.PhoneNumberConfirmed = userModel.PhoneNumberConfirmed;                    
                    user.SecurityStamp = userModel.SecurityStamp;
                    //user.TwoFactorEnabled = userModel.TwoFactorEnabled;
                    user.UserName = userModel.UserName;
                    // File Filtering and Upload
                    if (userModel.imgFile != null)
                    {
                        string ext = Path.GetExtension(userModel.imgFile.FileName).ToLower();
                        if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
                        {
                            string fileName = Path.GetFileNameWithoutExtension(userModel.imgFile.FileName);
                            string file = Guid.NewGuid() + "_" + fileName + ext;
                            var filePath = Path.Combine(hosting.WebRootPath, "images\\ProfilePhoto", file);
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                userModel.imgFile.CopyTo(fileStream);
                            }
                            user.Photo = "~/images/ProfilePhoto/" + file;
                        }
                    }


                }
                IdentityResult result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    TempData["Result"] = "Your Profile is Update Successfully.";
                }
                if (result.Errors.Count() > 0)
                {
                    foreach (var i in result.Errors)
                    {
                        ModelState.AddModelError(" ", i.ToString());
                    }
                    TempData["Result"] = result.Errors.ToString();
            }
            }
            catch (Exception ex)
            {

                TempData["Result"] = ex.Message;
            }

            return RedirectToAction("LoggedUserProfile");
        }

        // Details

        public async Task<IActionResult> Details(string id)
        {
          
            var user = await userManager.FindByIdAsync(id);
            return View(user);
        }
    }
}