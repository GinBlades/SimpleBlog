using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.ViewModel {
    public class PostDetailsViewModel {
        public Post Post { get; set; }
        public Comment Comment { get; set; }

        public PostDetailsViewModel(Post post) {
            Post = post;
            Comment = new Comment { PostId = Post.PostId };
        }
    }
}