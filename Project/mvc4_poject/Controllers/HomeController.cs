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
        public ActionResult Index()
        {
            var Top5fullpost= db.Posts.OrderByDescending(post => post.date).Take(4).ToList();
            var Top5intropost = new List<PostIntro>();
            foreach (var item in Top5fullpost)
            {
                var newitem = new PostIntro();
                char[] delimiterChars = {','};
                if (item.images != null )
                {
                    if (item.images.Contains(','))
                        newitem.image = item.images.Split(delimiterChars).First();
                    else
                        newitem.image = item.images;
                }
                else
                {
                    newitem.image="/Images/defualt/news.jpg";
                }
                newitem.id = item.id;
                newitem.title = item.title;
                newitem.category = item.category;
                Top5intropost.Add(newitem);
            }
            return View(Top5intropost);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult GetIntro(int Postid)
        {
            var mypost = (from e in db.Posts
                          where e.id == Postid
                         select e).First();
            return Json(new { intro = mypost.intro }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Denied()
        {
            return View();
        }
     
        public ActionResult Weather()
        {
            return View();
        }

        public ActionResult Search(string PostCat, string Stitle, string Sauthor)
        {
            var CatLst = new List<string>();

            var CatQry = from d in db.Posts
                           orderby d.category
                           select d.category;
            CatLst.AddRange(CatQry.Distinct());

            ViewBag.PostCat = new SelectList(CatLst);

            var posts = from m in db.Posts
                         select m;

            if (!String.IsNullOrEmpty(Stitle))
            {
                posts = posts.Where(s => s.title.Contains(Stitle));
            }

            if (!string.IsNullOrEmpty(PostCat)){
                posts = posts.Where(x => x.category == PostCat);
            }
            if(string.IsNullOrEmpty(Sauthor)){
                return View(posts);
            }else{
                return View(posts.Where(y => y.author.Contains(Sauthor)));
            }
        }
    }
}
