using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.Models
{
    public class ApplicationUserExpertChangeRequestExpert
    {
        public int ApplicationUserExpertChangeRequestId { get; set; }
        public ApplicationUserExpertChangeRequest ApplicationUserExpertChangeRequest { get; set; }
        public int ExpertId { get; set; }
        public Expert Expert { get; set; }
    }
}
