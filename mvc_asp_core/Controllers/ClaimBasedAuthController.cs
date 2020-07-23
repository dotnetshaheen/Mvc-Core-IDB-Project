using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mvc_asp_core.Models.UserIdentityModel;
using mvc_asp_core.ViewModel.ClaimModel;

namespace mvc_asp_core.Controllers
{
    public class ClaimBasedAuthController : Controller
    {
        private UserManager<IdentityModel> userManager;
        public ClaimBasedAuthController(UserManager<IdentityModel> user)
        {
            userManager = user;
        }
        public IActionResult Index()
        {
            return View();
        }

        // Display User And All Claim 

        public IActionResult Diplay()
        {
            ClaimVM claimVM = new ClaimVM();
            try
            {
                var userList = userManager.Users.ToList();
                ViewBag.user = new SelectList(userList, "UserName", "UserName");
                var AllClaims = ClaimStore.GetClaims;
                foreach (var c in AllClaims)
                {
                    UserClaim userClaims = new UserClaim
                    {
                        ClaimType = c.Type,
                        IsSelected = false
                    };
                    claimVM.userClaims.Add(userClaims);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View(claimVM);
        }

        [HttpPost]
        public async Task<IActionResult> Diplay(ClaimVM claim, string userList)
        {
            var user = await userManager.FindByEmailAsync(userList);
            var existingClaim = await userManager.GetClaimsAsync(user);
            foreach (var item in existingClaim)
            {
                await userManager.RemoveClaimAsync(user, item);
            }

            foreach (var c in claim.userClaims)
            {
                if (c.IsSelected)
                {
                    IdentityResult result = await userManager.AddClaimAsync(user, new Claim(c.ClaimType, c.ClaimType));
                    if (result.Succeeded)
                    {
                        ViewBag.Result = " Selcted claim on " + claim.Email + " successfully assigned";
                    }
                    if (result.Errors.Count()>0)
                    {
                        ViewBag.Result = "Claim Assign Failed";
                    }
                }
            }
            return View(claim);
        }
    }
}