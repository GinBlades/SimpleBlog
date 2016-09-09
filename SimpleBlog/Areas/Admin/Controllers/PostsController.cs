using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SimpleBlog.Models;
using Microsoft.AspNet.Identity;

namespace SimpleBlog.Areas.Admin.Controllers {

    [Authorize]
    public class PostsController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        public async Task<ActionResult> Index() {
            return View(await db.Post.OrderByDescending(p => p.Publish).Take(3).ToListAsync());
        }
        
        public async Task<ActionResult> Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Post.FindAsync(id);
            if (post == null) {
                return HttpNotFound();
            }
            return View(post);
        }
        
        public ActionResult Create() {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PostId,Title,Body,Publish,PostStatus,TagList")] Post post) {
            post.ApplicationUserId = User.Identity.GetUserId();
            ModelState.Clear();
            TryValidateModel(post);
            if (ModelState.IsValid) {
                post.Tags = new List<Tag>();
                foreach (var tagName in post.TagList.Split(',')) {
                    Tag tag = await db.Tag.FirstOrDefaultAsync(t => t.Name == tagName) ?? new Tag { Name = tagName };
                    post.Tags.Add(tag);
                }
                db.Post.Add(post);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(post);
        }
        
        public async Task<ActionResult> Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Post.FindAsync(id);
            if (post == null) {
                return HttpNotFound();
            }
            return View(post);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PostId,Title,Body,Publish,PostStatus,ApplicationUserId")] Post post) {
            if (ModelState.IsValid) {
                db.Entry(post).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(post);
        }
        
        public async Task<ActionResult> Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Post.FindAsync(id);
            if (post == null) {
                return HttpNotFound();
            }
            return View(post);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id) {
            Post post = await db.Post.FindAsync(id);
            db.Post.Remove(post);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
