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
    public class ApiActsController : ControllerBase
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public ApiActsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/ApiActs/{data}
        [HttpGet]
        [Route("~/api/ApiActs/{data}")]
        public async Task<IActionResult> Get(string data)
        {
            string result = "E_NOT_OK";
            var email = User.Identity.Name;
            string[] dataValues = data.Split('.');
            string vote = dataValues[0];
            string id = dataValues[1];

            ApplicationUser user = _context.Users.Include(u => u.ActVotes).Where(u => u.Email == email).FirstOrDefault();
            Act act = _context.Act.Where(a => a.Id.ToString() == id).FirstOrDefault();

            if ((null != user) && (null != act))
            {
                if (null == user.ActVotes.Where(av => av.Act == act).FirstOrDefault())
                {
                    ActApplicationUser actApplicationUser = new ActApplicationUser()
                    {
                        ApplicationUser = user,
                        Act = act,
                        Timestamp = DateTime.Now,
                        Vote = vote == "plus" ? 1 : -1,
                    };

                    _context.ActApplicationUsers.Add(actApplicationUser);
                    _context.SaveChanges();

                    result = "E_OK";
                }
            }

            return Ok(result);
        }

        // POST api/ApiActs
        [HttpPost]
        public void Post([FromBody] string value)
        {
            string value2 = value;
            value2 += "123";
        }
    }
}
