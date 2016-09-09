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

namespace SimpleBlog.Areas.Admin.Controllers {
    [Authorize]
    public class CommentsController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        public async Task<ActionResult> Index() {
            return View(await db.Comment.ToListAsync());
        }
        
        public async Task<ActionResult> Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comment.FindAsync(id);
            if (comment == null) {
                return HttpNotFound();
            }
            return View(comment);
        }
        
        public ActionResult Create() {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CommentId,Name")] Comment comment) {
            if (ModelState.IsValid) {
                db.Comment.Add(comment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(comment);
        }
        
        public async Task<ActionResult> Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comment.FindAsync(id);
            if (comment == null) {
                return HttpNotFound();
            }
            return View(comment);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CommentId,Name")] Comment comment) {
            if (ModelState.IsValid) {
                db.Entry(comment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(comment);
        }
        
        public async Task<ActionResult> Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comment.FindAsync(id);
            if (comment == null) {
                return HttpNotFound();
            }
            return View(comment);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id) {
            Comment comment = await db.Comment.FindAsync(id);
            db.Comment.Remove(comment);
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
