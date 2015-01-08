using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Chem.Models
{
    public class Movie
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Url { get; set; }
        public Reaction Reaction { get; set; }
        public UserProfile AddedBy { get; set; }
    }

    public class Reaction
    {
        public int ID { get; set; }
        public string Desc { get; set; }
        virtual public List<Reagent> Reagents { get; set; }
        public UserProfile AddedBy { get; set; }
    }

    public class Reagent
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public UserProfile AddedBy { get; set; }
        public List<Reaction> Reactions { get; set; }
    }

    public class MovieDBContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Reagent> Reagents { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
    }
}