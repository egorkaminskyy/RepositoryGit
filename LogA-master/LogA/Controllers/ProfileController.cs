using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LogA.Models;
using LogA.Models.Profile;
using LogA.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace LogA.Controllers
{
    public class ProfileController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private ApplicationDbContext context;
        private IHostingEnvironment appEnvironment;

        public ProfileController(ApplicationDbContext context, IHostingEnvironment appEnvironment, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.context = context;
            this.appEnvironment = appEnvironment;
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            ApplicationUser currentUser = await userManager.GetUserAsync(User);
            var profile = await context.Profiles.SingleOrDefaultAsync(m => m.User.Id == currentUser.Id);
            return View(profile);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit([Bind("Id,FirstName,SecondName,Phone,MyProperty,ProfilePicture, ProfilePictureFIle")] ProfileModel profileModel, IFormFile ProfilePictureFIle)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser currentUser = await userManager.GetUserAsync(User);
                if (ProfilePictureFIle != null)
                {

                    string path = Path.Combine(appEnvironment.WebRootPath, "ProfileImage");
                    Directory.CreateDirectory(Path.Combine(path, currentUser.Id));
                    string imagename = ProfilePictureFIle.FileName;
                    if (imagename.Contains('\\'))
                    {
                        imagename = imagename.Split('\\').Last();
                    }
                    using (FileStream fs = new FileStream(Path.Combine(path, currentUser.Id, imagename), FileMode.Create))
                    {
                        await ProfilePictureFIle.CopyToAsync(fs);
                    }
                    profileModel.ProfilePicture = imagename;
                }
                context.Update(profileModel);
                await context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
            
            
        }

        
        
    }
}
