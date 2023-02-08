using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.Models
{
    public class ActExpert
    {
        public int ActId { get; set; }
        public Act Act { get; set; }
        public int ExpertId { get; set; }
        public Expert Expert { get; set; }
    }
}
