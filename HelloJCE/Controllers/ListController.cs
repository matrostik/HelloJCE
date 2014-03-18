using HelloJCE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloJCE.Controllers
{
    public class ListController : Controller
    {
        //
        // GET: /List/
        public ActionResult Index() 
        {

            return View(new ItemRating());
        } 
 
        // 
        // GET: /HelloWorld/Welcome/ 

        public ActionResult Welcome(string name, int numTimes = 1)
        {
            ViewBag.Message = "Hello " + name;
            ViewBag.NumTimes = numTimes;

            return View();
        }

        public ActionResult Rate()
        {
            return View();
        }
        private MovieDBContext db = new MovieDBContext();
        [HttpPost]
        public ActionResult Rate(FormCollection form)
        {
            var rate = Convert.ToInt32(form["Score"]);
            var id = Convert.ToInt32(form["ArticleID"]);
            if (Request.Cookies["rating" + id] != null)
                return Content("false");
            Response.Cookies["rating" + id].Value = DateTime.Now.ToString();
            Response.Cookies["rating" + id].Expires = DateTime.Now.AddYears(1);
            ItemRating ir = IncrementArticleRating(rate, id);
            return Json(ir);
        }
        public ItemRating IncrementArticleRating(int rate, int id)
        {
            var mov = db.Movies.Where(a => a.ID == id).First();
            mov.Rating += rate;
            mov.TotalRaters += 1;
            db.SaveChanges();
            var ar = new ItemRating()
            {
                ItemID = mov.ID,
                Rating = mov.Rating,
                TotalRaters = mov.TotalRaters,
                AverageRating = Convert.ToDouble(mov.Rating) / Convert.ToDouble(mov.TotalRaters)
            };
            return ar;
        }
	}
}