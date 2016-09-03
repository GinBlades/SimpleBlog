using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models {
    public class Tag {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TagId { get; set; }
        [Required, MaxLength(64)]
        public string Name { get; set; }
        
        public ICollection<Post> Posts { get; set; }
    }
}