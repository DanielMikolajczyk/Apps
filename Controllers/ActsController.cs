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
            ICollection<Act> actList;

            if (User.HasClaim("Admin", "true"))
            {
                actList = await _context.Act.Include(a => a.ActVotes).ToListAsync();
            }
            else
            {
                actList = await _context.Act.Include(a => a.ActVotes).Where(a => a.Visible == true).ToListAsync();
            }

            return View(actList);
        }

        // GET: Acts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ICollection<Comment> comments = new List<Comment>();

            if (id == null)
            {
                return NotFound();
            }

            var act = await _context.Act
                .Include(a => a.Comments)
                .Include(a => a.ActVotes)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (act == null)
            {
                return NotFound();
            }

            if(null != act.Comments)
            {
                foreach(Comment comment in act.Comments)

                {
                    Comment dbComment = await _context.Comment.Include(c => c.ApplicationUser).Include(c => c.CommentVotes).FirstOrDefaultAsync(c => c.Id == comment.Id);
                    comments.Add(dbComment);
                }
            }

            ViewBag.Comments = comments;
            return View(act);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details()
        {
            Act act = _context.Act.Where(a => a.Id == Int16.Parse(Request.Form["ActId"]))
                .Include(a => a.Comments).FirstOrDefault();
            var email = User.Identity.Name;
            ApplicationUser user = _userManager.Users.Include(u => u.Comments).Where(u => u.UserName == email).FirstOrDefault();

            var comment = new Comment();
            comment.Text = Request.Form["Comment"];
            user.Comments.Add(comment);
            act.Comments.Add(comment);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: Acts/Create
        public async Task<ActionResult> Create()
        {
            ICollection<Expert> experts = await _context.Experts.ToListAsync();

            ViewBag.Experts = experts;
            return View();
        }

        // POST: Acts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Overview,Url")] Act act)
        {
            if (ModelState.IsValid)
            {
                act.Points = 0;

                string[] sExpertsIds = Request.Form["experts[]"].ToString().Split(',');
                /* Add Experts to Act model if they were selected */
                if (sExpertsIds != null)
                {
                    List<int> expertsIds = sExpertsIds.Select(int.Parse).ToList();
                    ICollection<Expert> experts = await _context.Experts.Where(e => expertsIds.Contains(e.Id)).ToListAsync();

                    ICollection<ActExpert> actExpertList = new List<ActExpert>();
                    foreach(Expert expert in experts)
                    {
                        actExpertList.Add(new ActExpert{
                            Act = act,
                            Expert = expert
                        });
                    }
                    act.ActExpert = actExpertList;
                }

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
            ViewBag.Experts = await _context.Experts.ToListAsync();

            return View(act);
        }

        // POST: Acts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Overview,Url,Visible")] Act act)
        {
            if (id != act.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                string[] sExpertsIds = Request.Form["experts[]"].ToString().Split(',');
                /* Add Experts to Act model if they were selected */
                if (sExpertsIds != null)
                {
                    List<int> expertsIds = sExpertsIds.Select(int.Parse).ToList();
                    ICollection<Expert> experts = await _context.Experts.Where(e => expertsIds.Contains(e.Id)).ToListAsync();

                    ICollection<ActExpert> actExpertList = new List<ActExpert>();
                    foreach (Expert expert in experts)
                    {
                        actExpertList.Add(new ActExpert
                        {
                            Act = act,
                            Expert = expert
                        });
                    }
                    act.ActExpert = actExpertList;
                }
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

        //Give all claims
        public async Task<IActionResult> giveClaims()
        {
            string email = HttpContext.User.Identity.Name;
            ApplicationUser user = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            var userClaims = await _userManager.GetClaimsAsync(user);

            foreach (var userClaim in userClaims)
            {
                await _userManager.RemoveClaimAsync(user, userClaim);
                }

            foreach (var claimName in ApplicationClaimTypes.AppClaimTypes)
            {
                var claim = new Claim(claimName, "true");
                await _userManager.AddClaimAsync(user, claim);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
