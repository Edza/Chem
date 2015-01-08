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
    

    public partial class ReactionController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        //
        // GET: /Reaction/

        public ActionResult Index()
        {
            return View(db.Reactions.ToList());
        }

        //
        // GET: /Reaction/Details/5

        public ActionResult Details(int id = 0)
        {
            Reaction reaction = db.Reactions.Find(id);
            if (reaction == null)
            {
                return HttpNotFound();
            }
            return View(reaction);
        }

        //
        // GET: /Reaction/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Reaction/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection formCollection, Reaction reaction)
        {
            if (ModelState.IsValid)
            {
                string reagentsRaw = formCollection["reagents"];
                List<int> reagentIds = ParseReagentInput(reagentsRaw);
                List<Reagent> reagents = GetReagentsByIds(reagentIds);
                reaction.Reagents = reagents;

                db.Reactions.Add(reaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reaction);
        } 

        //
        // GET: /Reaction/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Reaction reaction = db.Reactions.Find(id);
            if (reaction == null)
            {
                return HttpNotFound();
            }
            return View(reaction);
        }

        //
        // POST: /Reaction/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Reaction reaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reaction);
        }

        //
        // GET: /Reaction/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Reaction reaction = db.Reactions.Find(id);
            if (reaction == null)
            {
                return HttpNotFound();
            }
            return View(reaction);
        }

        //
        // POST: /Reaction/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reaction reaction = db.Reactions.Find(id);
            db.Reactions.Remove(reaction);
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