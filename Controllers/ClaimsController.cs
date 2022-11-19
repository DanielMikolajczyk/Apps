using Apps.Data;
using Apps.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.Controllers
{
    [Authorize (Policy = "IsAdmin")]
    public class ClaimsController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        public ClaimsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ClamisController
        public async Task<ActionResult> Index()
        {
            var claims = ApplicationClaimTypes.AppClaimTypes;

            ViewBag.claims = claims;

            return View();
        }

        // GET: ClamisController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ClamisController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClamisController/Create
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

        // GET: ClamisController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClamisController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: ClamisController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClamisController/Delete/5
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
