using mvc4_poject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc4_poject.Controllers
{
    public class HomeController : Controller
    {
        private PostDBContext db = new PostDBContext();
        private CommentDBContext db2 = new CommentDBContext();
        public ActionResult Index()
        {
            var allPosts = db.Posts;
            var allPostCommets = new List<PostComments>();
            var allComments = from m in db2.Comments
                              select m;
            foreach (var post in allPosts)
            {
                PostComments item = new PostComments();
                item.post = post;
                item.comments = allComments.Where(s => s.postId == post.id);
                item.numberOfCom = item.comments.ToArray<Comment>().Length;
                if (item.numberOfCom == 0)
                {
                    item.comments = null;
                }
                allPostCommets.Add(item);
            }
            var helper = new helperBlogController();
            helper.PostComments = allPostCommets;
            helper.newComment = new Comment();
            return View(helper);
        }

        public ActionResult addComment(int postId, string author, string body, string title, string urlAuthor)
        {
            Comment c = new Comment();
            c.author = author;
            c.body = body;
            c.postId = postId;
            c.title = title;

            db2.Comments.Add(c);
            db2.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
