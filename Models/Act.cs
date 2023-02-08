using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.Models
{
    public class Act
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public int Points { get; set; }
        public string Url { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public Pdf Pdf { get; set; }
        public ICollection<ActApplicationUser> ActVotes { get; set; }
        public ICollection<ActExpert> ActExpert { get; set; }
    }
}
