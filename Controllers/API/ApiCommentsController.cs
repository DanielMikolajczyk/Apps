using Apps.Data;
using Apps.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Apps.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiCommentsController : ControllerBase
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public ApiCommentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/ApiComments/{data}
        [HttpGet]
        [Route("~/api/ApiComments/{data}")]
        public async Task<IActionResult> Get(string data)
        {
            var email = User.Identity.Name;
            string[] dataValues = data.Split('.');
            string vote = dataValues[0];
            string id = dataValues[1];

            ApplicationUser user = await _context.Users.Include(u => u.CommentVotes).Where(u => u.Email == email).FirstOrDefaultAsync();
            Comment comment = await _context.Comment.Where(c => c.Id.ToString() == id).FirstOrDefaultAsync();

            if ((null != user) && (null != comment))
            {
                if (null == user.CommentVotes.Where(cv => cv.Comment == comment).FirstOrDefault())
                {
                    CommentApplicationUser commentApplicationUser = new CommentApplicationUser()
                    {
                        ApplicationUser = user,
                        Comment = comment,
                        Timestamp = DateTime.Now,
                        Vote = vote == "+" ? 1 : -1,
                    };

                    _context.CommentApplicationUsers.Add(commentApplicationUser);
                    await _context.SaveChangesAsync();

                    return Ok(new JsonResult(new { success = true, responseText = "Ok"}));
                }
            }

            return BadRequest(new JsonResult(new { success = false, responseText = "Error: Invalid parameters" }));
        }

/*        // GET: api/ApiComments/comments/{commentsIds}
        [HttpGet]
        [Route("~/api/ApiComments/comments/{commentsIds}")]
        public async Task<IActionResult> GetComments(string commentsIds)
        {
            var email = User.Identity.Name;
            string[] stringCommentsIds = commentsIds.Split('.');
            int[] intCommentsIds = Array.ConvertAll(stringCommentsIds, s => int.Parse(s));

            //TODO get comments that arent already loaded
            Comment comment = await _context.Comment.Where(c => c.Id.ToString() == id).FirstOrDefaultAsync();

            if ((null != user) && (null != comment))
            {
                if (null == user.CommentVotes.Where(cv => cv.Comment == comment).FirstOrDefault())
                {
                    CommentApplicationUser commentApplicationUser = new CommentApplicationUser()
                    {
                        ApplicationUser = user,
                        Comment = comment,
                        Timestamp = DateTime.Now,
                        Vote = vote == "+" ? 1 : -1,
                    };

                    _context.CommentApplicationUsers.Add(commentApplicationUser);
                    await _context.SaveChangesAsync();

                    return Ok(new JsonResult(new { success = true, responseText = "Ok" }));
                }
            }

            return BadRequest(new JsonResult(new { success = false, responseText = "Error: Invalid parameters" }));
        }*/

        // POST api/ApiActs
        [HttpPost]
        public void Post([FromBody] string value)
        {
            string value2 = value;
            value2 += "123";
        }
    }
}
