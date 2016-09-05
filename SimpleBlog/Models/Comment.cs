using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Models {
    public class Comment {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }
        [Required, DataType(DataType.EmailAddress), MaxLength(256)]
        public string Email { get; set; }
        [Required, DataType(DataType.MultilineText), MaxLength(2048)]
        public string Body { get; set; }
        [Required, HiddenInput(DisplayValue = false)]
        public int PostId { get; set; }
        public Post Post { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Required, HiddenInput(DisplayValue = false)]
        public CommentStatus CommentStatus { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ParentCommentId { get; set; }
        public Comment ParentComment { get; set; }

    }

    public enum CommentStatus {
        New, Reviewed, Approved, Denied, Flagged
    }
}