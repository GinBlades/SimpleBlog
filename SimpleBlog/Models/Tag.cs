using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models {
    public class Tag {
        [Required]
        public int TagId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}