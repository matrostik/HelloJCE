using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloJCE.Models
{
    public class ItemRating
    {
        public int ItemID { get; set; }
        public int Rating { get; set; }
        public int TotalRaters { get; set; }
        public double AverageRating { get; set; }
    }
}