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
    public class MovieController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        //
        // GET: /Movie/

        public ActionResult Index()
        {
            var accesableMovies = new List<Movie>();
            foreach (var item in db.Movies.ToList())
            {
                if (item.AddedById == WebSecurity.CurrentUserId || Accounts.IsAdmin())
                    accesableMovies.Add(item);
            }
            ViewBag.AccesableMovies = accesableMovies;

            return View(db.Movies.ToList());
        }

        //
        // GET: /Movie/Details/5

        public ActionResult Details(int id = 0)
        {
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        //
        // GET: /Movie/Create

        public ActionResult Create()
        {
            ViewBag.Reactions = db.Reactions.ToList();
            return View();
        }

        //
        // POST: /Movie/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection formCollection, Movie movie)
        {
            if (ModelState.IsValid)
            {
                var reaction =  db.Reactions.Find(int.Parse(formCollection["reaction"].Trim()));
                movie.Reaction = reaction;
                movie.AddedById = WebSecurity.CurrentUserId;
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        //
        // GET: /Movie/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            ViewBag.SelectedReaction = movie.Reaction.ID;
            ViewBag.Reactions = db.Reactions.ToList();

            return View(movie);
        }

        //
        // POST: /Movie/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection formCollection, Movie movie)
        {
            if (ModelState.IsValid)
            {
                var reaction = db.Reactions.Find(int.Parse(formCollection["reaction"].Trim()));
                db.Reactions.Attach(reaction);
                movie.Reaction = reaction;
                Movie savedMovie = movie;

                Movie movieToDelete = db.Movies.Find(movie.ID);
                db.Movies.Remove(movieToDelete);
                db.SaveChanges();
                db.Movies.Add(savedMovie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        //
        // GET: /Movie/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        //
        // POST: /Movie/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
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