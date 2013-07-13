using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TetkSys.Core.Data;

namespace TetkSys.Web.Controllers
{
    public class Projects2Controller : Controller
    {
		private TetkDbContext db = new TetkDbContext("tetkdbconn");

        //
        // GET: /Projects2/

        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        //
        // GET: /Projects2/Details/5

        public ActionResult Details(int id = 0)
        {
            ProjectInfo projectinfo = db.Projects.Find(id);
            if (projectinfo == null)
            {
                return HttpNotFound();
            }
            return View(projectinfo);
        }

        //
        // GET: /Projects2/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Projects2/Create

        [HttpPost]
        public ActionResult Create(ProjectInfo projectinfo)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(projectinfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(projectinfo);
        }

        //
        // GET: /Projects2/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ProjectInfo projectinfo = db.Projects.Find(id);
            if (projectinfo == null)
            {
                return HttpNotFound();
            }
            return View(projectinfo);
        }

        //
        // POST: /Projects2/Edit/5

        [HttpPost]
        public ActionResult Edit(ProjectInfo projectinfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectinfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(projectinfo);
        }

        //
        // GET: /Projects2/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ProjectInfo projectinfo = db.Projects.Find(id);
            if (projectinfo == null)
            {
                return HttpNotFound();
            }
            return View(projectinfo);
        }

        //
        // POST: /Projects2/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectInfo projectinfo = db.Projects.Find(id);
            db.Projects.Remove(projectinfo);
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