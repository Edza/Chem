using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Chem.Models
{
    public class Movie
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(200)]
        public string Desc { get; set; }
        [Required]
        [StringLength(20)]
        public string Url { get; set; }
        virtual public Reaction Reaction { get; set; }
        public int AddedById { get; set; }
    }

    public class Reaction
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Desc { get; set; }
        virtual public List<Reagent> Reagents { get; set; }
        public int AddedById { get; set; }
    }

    public class Reagent
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int AddedById { get; set; }
        public List<Reaction> Reactions { get; set; }
    }

    public class MovieDBContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Reagent> Reagents { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
    }
}