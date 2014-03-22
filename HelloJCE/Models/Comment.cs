using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelloJCE.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public int MovieId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", HtmlEncode = true, ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        [Display(Name = "Name")]
        [Required]
        public string UserName { get; set; }

        [StringLength(2048, ErrorMessage = "Comment too long.")]
        [Display(Name = "Comment")]
        [Required]
        public string Text { get; set; }

        public int Rating { get; set; }
    }
}