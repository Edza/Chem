using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chem.Models;

namespace Chem.Controllers
{
    public class ReagentController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        //
        // GET: /Reagent/

        public ActionResult Index()
        {
            return View(db.Reagents.ToList());
        }

        //
        // GET: /Reagent/List
        public ActionResult List()
        {
            return Json(db.Reagents.ToList(), JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Reagent/Details/5

        public ActionResult Details(int id = 0)
        {
            Reagent reagent = db.Reagents.Find(id);
            if (reagent == null)
            {
                return HttpNotFound();
            }
            return View(reagent);
        }

        //
        // GET: /Reagent/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Reagent/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reagent reagent)
        {
            if (ModelState.IsValid)
            {
                db.Reagents.Add(reagent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reagent);
        }

        //
        // GET: /Reagent/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Reagent reagent = db.Reagents.Find(id);
            if (reagent == null)
            {
                return HttpNotFound();
            }
            return View(reagent);
        }

        //
        // POST: /Reagent/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Reagent reagent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reagent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reagent);
        }

        //
        // GET: /Reagent/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Reagent reagent = db.Reagents.Find(id);
            if (reagent == null)
            {
                return HttpNotFound();
            }
            return View(reagent);
        }

        //
        // POST: /Reagent/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reagent reagent = db.Reagents.Find(id);
            db.Reagents.Remove(reagent);
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