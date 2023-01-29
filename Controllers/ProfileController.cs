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

        public ProfileController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Profile/
        public async Task<IActionResult> Index()
        {
            string email = HttpContext.User.Identity.Name;
            ApplicationUser user = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
                
            return View(user);
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

        // GET: Profile/ExpertRequest
        public async Task<ActionResult> ExpertRequest()
        {
            string email = HttpContext.User.Identity.Name;
            ApplicationUser user = await _context.Users.Include(u => u.ApplicationUserExperts).Where(u => u.Email == email).FirstOrDefaultAsync();
            ICollection<Expert> expertsDb = await _context.Experts.ToListAsync();

            ViewBag.Experts = expertsDb;

            return View(user);
        }

        // POST: Profile/ExpertRequest
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExpertRequest(ApplicationUser userRequest)
        {
            string email = HttpContext.User.Identity.Name;
            ApplicationUser user = await _context.Users.Include(u => u.ApplicationUserExperts).Where(u => u.Email == email).FirstOrDefaultAsync();
            ICollection<Expert> expertsDb = await _context.Experts.ToListAsync();
            ICollection<Expert> expertChangeList = new List<Expert>();
            string[] expertsForm = Request.Form["Expert[]"];

            foreach(string id in expertsForm)
            {
                if(expertsDb.Any(e => e.Id.ToString() == id))
                {
                    expertChangeList.Add(expertsDb.Where(e => e.Id.ToString() == id).First());
                }
            }

            ApplicationUserExpertChangeRequest applicationUserExpertChangeRequest = new ApplicationUserExpertChangeRequest()
            {
                ApplicationUser = user,
            };
            await _context.AddAsync(applicationUserExpertChangeRequest);

            ICollection<ApplicationUserExpertChangeRequestExpert> applicationUserExpertChangeRequestExperts = new List<ApplicationUserExpertChangeRequestExpert>();
            foreach(Expert expert in expertChangeList)
            {
                applicationUserExpertChangeRequestExperts.Add(new ApplicationUserExpertChangeRequestExpert()
                {
                    ApplicationUserExpertChangeRequest = applicationUserExpertChangeRequest,
                    Expert = expert,
                }
                );
            }
            await _context.AddRangeAsync(applicationUserExpertChangeRequestExperts);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
