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

namespace Apps.Controllers
{
    public class PdfsController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public PdfsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Pdfs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pdf.ToListAsync());
        }

        // GET: Pdfs/Download
        public IActionResult Download()
        {
            return View();
        }

        // POST: Acts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DownloadPdf()
        {
            string actId = Request.Form["ActId"];
            string year = Request.Form["Year"];
            if (ModelState.IsValid)
            {
                ActDownloader actDownloader = new ActDownloader(_context);

                if("on" == Request.Form["All"])
                {
                    actDownloader.downloadAll(year);
                }
                else if(("" != actId) && ("" != year))
                {
                    actDownloader.download(Int16.Parse(actId));
                }

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // POST: Acts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DownloadPdfLink()
        {
            string link = Request.Form["Link"];
            if (ModelState.IsValid)
            {
                ActDownloader actDownloader = new ActDownloader(_context);

                if (link.StartsWith("https://dziennikustaw.gov.pl/D"))
                {
                    actDownloader.downloadLink(link);

                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Download()
        //{
        //    Act act = _context.Act.Where(a => a.Id == Int16.Parse(Request.Form["ActId"]))
        //        .Include(a => a.Comments).FirstOrDefault();
        //    var email = HttpContext.User.Identity.Name;
        //    ApplicationUser user = _userManager.Users.Where(u => u.UserName == email).FirstOrDefault();

        //    var comment = new Comment();
        //    comment.Text = Request.Form["Comment"];
        //    user.Comments.Add(comment);
        //    act.Comments.Add(comment);

        //    _context.SaveChanges();

        //    return RedirectToAction(nameof(Index));
        //}
    }
}
