using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<ApplicationUserDataChangeRequest> ApplicationUserDataChangeRequests { get; set; }
        public ICollection<ApplicationUserExpert> ApplicationUserExperts { get; set; }
        public ICollection<ApplicationUserExpertChangeRequest> ApplicationUserExpertChangeRequests { get; set; }
        public ICollection<ActApplicationUser> ActVotes { get; set; }
    }
}
