﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.Models
{
    public class ApplicationUserExpertChangeRequest
    {
        [Key]
        public int id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<ApplicationUserExpertChangeRequestExpert> ApplicationUserExpertChangeRequestExperts { get; set; }

    }
}
