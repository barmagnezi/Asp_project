using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc4_poject.Models;

namespace mvc4_poject.Controllers
{
    public class ManagerController : Controller
    {
        private PostDBContext db = new PostDBContext();
        private CommentDBContext db2 = new CommentDBContext();
        private ReporterDBContext dbReporter = new ReporterDBContext();

        //
        // GET: /Post/

        public ActionResult Index()
        {
            return View(db.Posts.ToList());
        }

        //
        // GET: /Post/Details/5

        public ActionResult Details(long id = 0)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        //
        // GET: /Post/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Post/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                var allrep = from m in dbReporter.Reporter
                                  select m;
                var search = allrep.Where(s => s.name == post.author);
                post.date = DateTime.Now.ToString();
                /*
                if(search.ToArray<Reporter>().Length==0)
                    return RedirectToAction("Edit",post);*/
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }


        //
        // GET: /Post/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        //
        // POST: /Post/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        //
        // GET: /Post/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        //
        // POST: /Post/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //comments
        public ActionResult Comments(long id = 0)
        {

            Console.WriteLine("id:" + id);
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            var allComments = from m in db2.Comments
                              select m;

            allComments = allComments.Where(s => s.postId == id);
            if (allComments == null)
                return HttpNotFound();
            return View(allComments);
        }

        public ActionResult DeleteComment(long PostId = 0, long CommentId = 0)
        {
            Comment com = db2.Comments.Find(CommentId);
            if (com == null)
            {
                return HttpNotFound();
            }
            db2.Comments.Remove(com);
            db2.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}