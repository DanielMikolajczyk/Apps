using Apps.Data;
using Apps.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Apps.Controllers
{
    [Authorize (Policy = "IsAdmin")]
    public class AdministrationController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public AdministrationController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: AdministrationController
        public async Task<ActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            return View(users);
        }

        // GET: AdministrationController/Details/5
        [Authorize(Policy = "IsExpert")]
        public async Task<ActionResult> Details(string id)
        {
            var user = await _userManager.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

            return View(user);
        }

        // GET: AdministrationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdministrationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdministrationController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userClaims = await _userManager.GetClaimsAsync(user);
            var claims = ApplicationClaimTypes.AppClaimTypes;

            ViewBag.userClaims = userClaims;
            ViewBag.claims = claims;

            return View(user);
        }

        // POST: AdministrationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, IFormCollection collection)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                var userClaims = await _userManager.GetClaimsAsync(user);
                foreach (var userClaim in userClaims)
                {
                    await _userManager.RemoveClaimAsync(user, userClaim);
                }

                foreach (var claimId in Request.Form.Keys)
                {
                    if(claimId.All(char.IsNumber))
                    {
                        var claim = new Claim(ApplicationClaimTypes.AppClaimTypes[Int16.Parse(claimId)].ToString(),"true");
                        await _userManager.AddClaimAsync(user, claim);
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdministrationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdministrationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
