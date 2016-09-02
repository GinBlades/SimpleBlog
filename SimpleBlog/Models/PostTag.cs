using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models {
    public class PostTag {
        [Required]
        public int PostId { get; set; }
        [Required]
        public int TagId { get; set; }
    }
}