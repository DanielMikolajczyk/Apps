using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Apps.Data;
using Apps.Models;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Apps.Services;
using Apps.Services.Downloaders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Apps.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public ProfileController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Profile/
        public async Task<IActionResult> Index()
        {
            //if (_signInManager.IsSignedIn(User))
            //{
                string email = HttpContext.User.Identity.Name;
                ApplicationUser user = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
                
                return View(user);
            //}
            
            //return RedirectToAction("Index", "Login");
        }

        // GET: Profile/Edit
        public async Task<ActionResult> Edit()
        {
            string email = HttpContext.User.Identity.Name;
            ApplicationUser user = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();

            return View(user);
        }

        // POST: Profile/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("Id,FirstName,Surname,MiddleName,Email,PhoneNumber")] ApplicationUser userRequest)
        {
            string email = HttpContext.User.Identity.Name;
            ApplicationUser user = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();

            if (ModelState.IsValid)
            {
                ApplicationUserDataChangeRequest userDataChangeRequest = new ApplicationUserDataChangeRequest();
                userDataChangeRequest.FirstName = userRequest.FirstName;
                userDataChangeRequest.Surname = userRequest.Surname;
                userDataChangeRequest.MiddleName = userRequest.MiddleName;
                userDataChangeRequest.Email = userRequest.Email;
                userDataChangeRequest.PhoneNumber = userRequest.PhoneNumber;
                userDataChangeRequest.ApplicationUser = user;

                _context.Add(userDataChangeRequest);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

    }
}
