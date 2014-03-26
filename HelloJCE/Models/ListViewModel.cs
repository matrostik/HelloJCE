using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloJCE.Models
{
    public class ListViewModel
    {

        public ItemRating rating { get; set; }

        public string item { get; set; }
        public List<SelectListItem> items { get; set; }
    }
}