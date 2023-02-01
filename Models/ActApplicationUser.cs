using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.Models
{
    public class ActApplicationUser
    {
        public int ActId { get; set; }
        public Act Act { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int Vote { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
