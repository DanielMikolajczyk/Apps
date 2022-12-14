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

namespace Apps.Controllers
{
    public class ActsController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public ActsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Acts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Act.ToListAsync());
        }

        // GET: Acts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var act = await _context.Act
                .Include(a => a.Comments)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (act == null)
            {
                return NotFound();
            }

            return View(act);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details()
        {
            var sth = Request.Form;
            Act act = _context.Act.Where(a => a.Id == Int16.Parse(Request.Form["ActId"]))
                .Include(a => a.Comments).FirstOrDefault();
            var email = HttpContext.User.Identity.Name;
            var user = _userManager.Users.Where(u => u.UserName == email).FirstOrDefault();

            var comment = new Comment();
            comment.Text = Request.Form["Comment"];

            act.Comments.Add(comment);
            //user.Comments.Add(comment);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: Acts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Acts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Overview,Points,Url")] Act act)
        {
            if (ModelState.IsValid)
            {
                _context.Add(act);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(act);
        }

        // GET: Acts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var act = await _context.Act.FindAsync(id);
            if (act == null)
            {
                return NotFound();
            }
            return View(act);
        }

        // POST: Acts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Overview,Points,Url")] Act act)
        {
            if (id != act.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(act);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActExists(act.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(act);
        }

        // GET: Acts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var act = await _context.Act
                .FirstOrDefaultAsync(m => m.Id == id);
            if (act == null)
            {
                return NotFound();
            }

            return View(act);
        }

        // POST: Acts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var act = await _context.Act.FindAsync(id);
            _context.Act.Remove(act);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActExists(int id)
        {
            return _context.Act.Any(e => e.Id == id);
        }
    }
}
