using SimpleBlog.Models;
using SimpleBlog.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Controllers {
    public class PostsController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Posts
        public async Task<ActionResult> Index() {
            return View(await db.Post.Where(p => p.PostStatus == PostStatus.Publish)
                .Include(p => p.Comments)
                .OrderByDescending(p => p.Publish).Take(3).ToListAsync());
        }
        public async Task<ActionResult> Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Post.FindAsync(id);
            if (post == null) {
                return HttpNotFound();
            }
            return View(new PostDetailsViewModel(post));
        }
    }
}