using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace HelloJCE.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [StringLength(75, ErrorMessage = "Title cannot be longer than 75 characters.")]
        public string Title { get; set; }

        [StringLength(150, ErrorMessage = "Image cannot be longer than 150 characters.")]
        public string Image { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}",HtmlEncode=true, ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        [StringLength(50, ErrorMessage = "Genre cannot be longer than 50 characters.")]
        public string Genre { get; set; }

        public decimal Price { get; set; }

        //fields for rating
        public int Rating { get; set; }

        public int TotalRaters { get; set; }

        public virtual List<Comment> Comments { get; set; }
    }
    public class MovieDBContext : DbContext
    {
        public MovieDBContext()
            : base("DefaultConnection")
        {
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}