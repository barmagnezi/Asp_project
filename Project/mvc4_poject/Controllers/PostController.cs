using mvc4_poject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc4_poject.Controllers
{
    public class PostController : Controller
    {
        private PostDBContext db = new PostDBContext();
        private CommentDBContext db2 = new CommentDBContext();
        private ReporterDBContext dbRep = new ReporterDBContext();

        public ActionResult Index()
        {
            var fullpost = db.Posts.OrderByDescending(post => post.date).ToList();
            var intropost = new List<PostIntro>();
            foreach (var item in fullpost)
            {
                var newitem = new PostIntro();
                char[] delimiterChars = { ',' };
                if (item.images != null)
                {
                    if (item.images.Contains(','))
                        newitem.image = item.images.Split(delimiterChars).First();
                    else
                        newitem.image = item.images;
                }
                else
                {
                    newitem.image = "Images/defualt/news.jpg";
                }
                newitem.id = item.id;
                newitem.title = item.title;
                newitem.category = item.category;
                intropost.Add(newitem);
            }
            return View(intropost);
        }
        /*public ActionResult Index()
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
        }*/
        public ActionResult FullPost(int id){
            var allPosts = (from m in db.Posts
                            select m).Where(s => s.id == id);
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

        public ActionResult addComment(int postId, string author, string body, string title)
        {
            Comment c = new Comment();
            c.author = author;
            c.body = body;
            c.postId = postId;
            c.title = title;

            db2.Comments.Add(c);
            db2.SaveChanges();
            return RedirectToAction("FullPost/"+postId);
        }

        public ActionResult AuthorPosts()
        {
            /*doesn't work!!!
             * 1
             * var query =dbRep.Reporter.Join(db.Posts,
                p=>p.ID,
                r=>Convert.ToInt32(r.IdAuthor),
                (p,r)=>new{
                    reporter=p,
                    post=r
                }).ToArray();
            2!!
            var qq=(from s in db.Posts
                   join reporter in dbRep.Reporter on s.IdAuthor equals reporter.ID.ToString()
                   select new{rep=reporter,pos=s}).ToList();
             * 
             * 3!!!
             *  /*var query = (db.Posts).      // source
           Join(dbRep.Reporter,         // target
              c => c.IdAuthor,          // FK-Foreign key
              cm => cm.ID.ToString(),   // PK-Primary key
              (c, cm) => new { AuthorID = cm.ID, AuthorName = cm.name, title = c.title, PostId = c.id }).ToList();  // project result*/
            List<ReporterAndPosts> All = new List<ReporterAndPosts>();
            foreach(var post in db.Posts){
                Reporter rep;
                if(post.IdAuthor=="0"){
                        rep=new Reporter();
                        rep.ID=0;
                        rep.phone="none";
                        rep.email="none";
                        rep.name=post.author;
                }else
                    rep=dbRep.Reporter.Find(Convert.ToInt32(post.IdAuthor));
                var item=new ReporterAndPosts();
                item.rep=rep;
                item.posts=post;
                All.Add(item);
            }

          

            return View(All);
        }

        public ActionResult statistics()
        {
            return View();
        }

        public ActionResult getStatistics()
        {
            List<int> Adata = new List<int>();
            List<string> Anames = new List<string>();
            var a = (from m in db.Posts
                     select m).GroupBy(g => g.category).Select(r => new { Id = r.Key, Count = r.Count() }).ToList();
            foreach (var item in a)
            {
                Adata.Add(item.Count);
                Anames.Add(item.Id);
            }
            return Json(new { names = Anames, Sdata = Adata.ToArray<int>(), len = Anames.ToArray().Length }, JsonRequestBehavior.AllowGet);
        }
    }
}
