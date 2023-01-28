using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.Models
{
    public class ApplicationUserDataChangeRequest
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public virtual string? Email { get; set; }

        public virtual string? PhoneNumber { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
