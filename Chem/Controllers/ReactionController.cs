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
using Chem.Controllers.Utility;

namespace Chem.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public partial class ReactionController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        //
        // GET: /Reaction/

        public ActionResult Index()
        {
            var accesableReactions = new List<Reaction>();
            foreach (var item in db.Reactions.ToList())
            {
                if (item.AddedById == WebSecurity.CurrentUserId || Accounts.IsAdmin())
                    accesableReactions.Add(item);
            }
            ViewBag.AccesableReactions = accesableReactions;

            return View(db.Reactions.ToList());
        }

        //
        // GET: /Reaction/Details/5

        public ActionResult Details(int id = 0)
        {
            db.Reagents.ToList();
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
                reaction.AddedById = WebSecurity.CurrentUserId;
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
        public ActionResult Edit(FormCollection formCollection, Reaction reaction)
        {
            if (ModelState.IsValid)
            {
                string reagentsRaw = formCollection["reagents"];
                List<int> reagentIds = ParseReagentInput(reagentsRaw);
                List<Reagent> reagents = GetReagentsByIds(reagentIds);
                reaction.Reagents = reagents;


                Reaction reactionToRemoveReagentsFor = db.Reactions.Find(reaction.ID);
                reactionToRemoveReagentsFor.Reagents.RemoveAll(x => true);
                reactionToRemoveReagentsFor.Desc = reaction.Desc;
                foreach (var reagent in reagents)
                {
                    db.Reagents.Attach(reagent);
                    reactionToRemoveReagentsFor.Reagents.Add(reagent);
                }


                db.SaveChanges();

                //Reaction reactionToRemove = db.Reactions.Find(reaction.ID);
                //db.Reactions.Remove(reactionToRemove);
                //db.SaveChanges();
                //db.Reactions.Add(reaction);
                //db.SaveChanges();

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