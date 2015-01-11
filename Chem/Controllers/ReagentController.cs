using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chem.Models;
using Chem.Filters;
using WebMatrix.WebData;
using System.Web.Security;
using Chem.Controllers.Utility;

namespace Chem.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class ReagentController : Controller
    {
        private MovieDBContext db = new MovieDBContext();
        private UsersContext dbUsers = new UsersContext();

        //
        // GET: /Reagent/

        public ActionResult Index()
        {
            var accesableReagents = new List<Reagent>();
            foreach (var item in db.Reagents.ToList())
            {
                if (item.AddedById == WebSecurity.CurrentUserId || Accounts.IsAdmin())
                    accesableReagents.Add(item);
            }
            ViewBag.AccesableReagents = accesableReagents;
            return View(db.Reagents.ToList());
        }

        //
        // GET: /Reagent/List
        [AllowAnonymous]
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
                reagent.AddedById = WebSecurity.CurrentUserId;
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

            int addedBy = reagent.AddedById;
            if (!Accounts.CanDoThis(addedBy))
                return Redirect("/");

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
                Reagent reagentById = db.Reagents.Find(reagent.ID);
                reagentById.Name = reagent.Name;

                db.Entry(reagentById).State = EntityState.Modified;
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

            

            int addedBy = reagent.AddedById;
            if (!Accounts.CanDoThis(addedBy))
                return Redirect("/");


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

            int addedBy = db.Reagents.Find(reagent.ID).AddedById;
            if (!Accounts.CanDoThis(addedBy))
                return Redirect("/");

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