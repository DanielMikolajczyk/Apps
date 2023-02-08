using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.Models
{
    public class Expert
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ApplicationUserExpert> ApplicationUserExperts { get; set; }
        public ICollection<ApplicationUserExpertChangeRequestExpert> ApplicationUserExpertChangeRequestExperts{ get; set; }
        public ICollection<ActExpert> ActExperts { get; set; }
    }
}
