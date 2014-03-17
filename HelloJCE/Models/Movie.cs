using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace HelloJCE.Models
{
    public class Movie
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
    }
    public class MovieDBContext : DbContext
    {
        public MovieDBContext()
            : base("DefaultConnection")
        {
        }
        public DbSet<Movie> Movies { get; set; }
    }
}