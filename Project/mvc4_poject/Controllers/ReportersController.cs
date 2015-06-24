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
    public class ReportersController : Controller
    {
        private ReporterDBContext db = new ReporterDBContext();

        //
        // GET: /Reporters/

        public ActionResult Index()
        {
            return View(db.Reporter.ToList());
        }

        //
        // GET: /Reporters/Details/5

        public ActionResult Details(long id = 0)
        {
            Reporter reporter = db.Reporter.Find(id);
            if (reporter == null)
            {
                return HttpNotFound();
            }
            return View(reporter);
        }

        //
        // GET: /Reporters/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Reporters/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reporter reporter)
        {
            if (ModelState.IsValid)
            {
                db.Reporter.Add(reporter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reporter);
        }

        //
        // GET: /Reporters/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Reporter reporter = db.Reporter.Find(id);
            if (reporter == null)
            {
                return HttpNotFound();
            }
            return View(reporter);
        }

        //
        // POST: /Reporters/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Reporter reporter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reporter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reporter);
        }

        //
        // GET: /Reporters/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Reporter reporter = db.Reporter.Find(id);
            if (reporter == null)
            {
                return HttpNotFound();
            }
            return View(reporter);
        }

        //
        // POST: /Reporters/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Reporter reporter = db.Reporter.Find(id);
            db.Reporter.Remove(reporter);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}