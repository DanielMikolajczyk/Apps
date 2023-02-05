using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.Models
{
    public class CommentApplicationUser
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        public int Vote { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
