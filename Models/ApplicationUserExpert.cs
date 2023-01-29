using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.Models
{
    public class ApplicationUserExpert
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int ExpertId { get; set; }
        public Expert Expert { get; set; }
    }
}
