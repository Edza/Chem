using Chem.Controllers.Utility;
using Chem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chem.Controllers
{
    public  partial class HomeController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        public ActionResult Index(string title = null, string reagents = null)
        {
            IEnumerable<Movie> movies;

            if (title != null && title.Trim() != "")
            {
                movies = from m in db.Movies
                             where m.Title.Contains(title)
                             select m;

                ViewBag.Query = title;
            }
            else if (reagents != null && reagents.Trim() != "")
            {
                List<int> reagentIds = ParseReagentInput(reagents);
                List<Reagent> reagentsList = GetReagentsByIds(reagentIds);

                List<Movie> allMovies = db.Movies.ToList();
                List<Movie> moviesList = new List<Movie>();

                foreach (var movie in allMovies)
                {
                    if (reagentIds.All(r => movie.Reaction.Reagents.Exists(r2 => r == r2.ID)))
                        moviesList.Add(movie);
                }

                movies = moviesList;
                ViewBag.Query = reagentsList.Aggregate<Reagent, string>("", (tog, th) => tog + " " + th.Name);
            }
            else
            {
                movies = from m in db.Movies
                             orderby m.ID descending
                             select m;

                movies = movies.Take(5);

                ViewBag.Query = "";
            }

            ViewBag.Movies = movies.ToList();

            return View();
        }

        public ActionResult Lang(bool isEnglish)
        {
            Session["IsEnglish"] = isEnglish;
            Local.SetCurrLang(isEnglish);

            return Redirect("/");
        }
    }
}
