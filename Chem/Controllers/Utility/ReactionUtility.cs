﻿using Chem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chem.Controllers
{
    public partial class ReactionController : Controller
    {
        private List<int> ParseReagentInput(string reagents)
        {
            return reagents.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Where(item => item.Trim() != "")
                .Select(x => int.Parse(x.Trim()))
                .ToList();
        }

        private List<Reagent> GetReagentsByIds(List<int> reagentIds)
        {
            var reagents = from r in db.Reagents
                               where reagentIds.Contains(r.ID) == true
                               select r;
          
            return reagents.ToList();
        }
    }

    public partial class HomeController : Controller
    {
        private List<int> ParseReagentInput(string reagents)
        {
            return reagents.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Where(item => item.Trim() != "")
                .Select(x => int.Parse(x.Trim()))
                .ToList();
        }

        private List<Reagent> GetReagentsByIds(List<int> reagentIds)
        {
            var reagents = from r in db.Reagents
                           where reagentIds.Contains(r.ID) == true
                           select r;

            return reagents.ToList();
        }
    }
}