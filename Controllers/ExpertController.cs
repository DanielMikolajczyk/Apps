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
    [Authorize(Policy = "IsExpert")]
    public class ExpertController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public ExpertController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Expert
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _context.Users.Include(c => c.ApplicationUserExperts).Where(u => u.Email == User.Identity.Name).FirstOrDefaultAsync();
            ICollection<Expert> experts = await _context.ApplicationUserExperts.Include(aue => aue.ApplicationUser).Include(aue => aue.Expert).Where(aue => aue.ApplicationUserId == user.Id).Select(aue => aue.Expert).ToListAsync();
            List<Act> expertActs = new List<Act>();

            foreach(Expert expert in experts)
            {
                var acts = await _context.ActExperts.Include(ae => ae.Act).Include(ae => ae.Expert).Where(ae => ae.ExpertId == expert.Id).Select(ae => ae.Act).ToListAsync();
                
                if(null != acts)
                {
                    expertActs.AddRange(acts);
                }
            }

            ViewBag.User = user;
            ViewBag.UserExperts = experts;
            return View(expertActs);
        }


    }
}
