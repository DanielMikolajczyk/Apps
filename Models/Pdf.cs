using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.Models
{
    public class Pdf
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public int ActId { get; set; }
        public Act Act { get; set; }
    }
}
