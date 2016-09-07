using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Controllers {
    public class HomeController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();
        public async Task<ActionResult> Index() {
            List<Post> posts = await db.Post.ToListAsync();
            return View(posts);
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}