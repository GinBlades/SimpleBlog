using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Models {
    public class Post {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }
        [Required, MaxLength(256)]
        public string Title { get; set; }
        [Required, DataType(DataType.MultilineText)]
        public string Body { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime Publish { get; set; }
        [Required]
        public PostStatus PostStatus { get; set; }
        [Required, HiddenInput(DisplayValue = false)]
        public string ApplicationUserId { get; set; }
        [NotMapped]
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }

    public enum PostStatus {
        Draft, Review, Publish, Archive
    }
}