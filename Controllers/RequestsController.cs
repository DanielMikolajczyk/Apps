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
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Apps.Controllers
{
    [Authorize(Policy = "IsAdmin")]
    public class RequestsController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public RequestsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Request
        public async Task<ActionResult> Index()
        {
            ViewBag.UserDataChangeRequests  = await _context.ApplicationUserDataChangeRequests.Include(r => r.ApplicationUser).ToListAsync();
            ViewBag.ExpertChangeRequests = await _context.ApplicationUserExpertChangeRequests.Include(r => r.ApplicationUser).ToListAsync();

            return View();
        }

        // GET: Requests/UserData/{id}
        public async Task<ActionResult> UserData(int? id)
        {
            ApplicationUserDataChangeRequest request = await _context.ApplicationUserDataChangeRequests.Include(r => r.ApplicationUser).Where(r => r.Id == id).FirstOrDefaultAsync();

            return View(request);
        }

        // POST: Requests/UserData/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserData(ApplicationUserDataChangeRequest request)
        {
            ApplicationUser user = await _context.Users.Where(u => u.Id == Request.Form["UserId"].ToString()).FirstOrDefaultAsync();

            user.FirstName = Request.Form["FirstName"].ToString();
            user.Surname = Request.Form["Surname"].ToString();
            user.MiddleName = Request.Form["MiddleName"].ToString();
            user.PhoneNumber = Request.Form["PhoneNumber"].ToString();

            _context.ApplicationUserDataChangeRequests.Remove(request);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Requests/Expert/{id}
        public async Task<ActionResult> Expert(int? id)
        {
            ApplicationUserExpertChangeRequest request = await _context.ApplicationUserExpertChangeRequests.
                Include(r => r.ApplicationUser).Include(r => r.ApplicationUserExpertChangeRequestExperts).Where(r => r.id == id).FirstOrDefaultAsync();

            //var requestData = await _context.ApplicationUserExpertChangeRequestExperts.Include(r => r.ApplicationUserExpertChangeRequest)
            //    .Include(r => r.Expert).Where(u => u.ApplicationUserExpertChangeRequestId == request.id).ToListAsync();

            ViewBag.Experts = await _context.Experts.ToListAsync();

            return View(request);
        }

        // POST: Requests/Expert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Expert(ApplicationUserExpertChangeRequest request)
        {
            ApplicationUser user = await _context.Users.Where(u => u.Id == Request.Form["UserId"].ToString()).Include(u => u.ApplicationUserExperts).FirstOrDefaultAsync();
            string[] experts = Request.Form["Expert[]"];
            ICollection<Expert> expertsDb = await _context.Experts.ToListAsync();
            ICollection<ApplicationUserExpert> userExpertsList = new List<ApplicationUserExpert>();

            foreach (string id in experts)
            {
                if(expertsDb.Any(e => e.Id.ToString() == id))
                {
                    userExpertsList.Add(new ApplicationUserExpert()
                    {
                        ApplicationUser = user,
                        Expert = expertsDb.FirstOrDefault(e => e.Id.ToString() == id)
                    });
                }
            }

            user.ApplicationUserExperts = userExpertsList;
            _context.ApplicationUserExpertChangeRequests.Remove(request);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
