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
            ItemRating ir = new ItemRating()
            {
                ItemID = 1,
                Rating = 8,
                TotalRaters = 2,
                AverageRating = 4
            };
            return View(ir);
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


        public ActionResult RateItem(int id, int rate)
        {
            int userId = 142; // WebSecurity.CurrentUserId;
            bool success = false;
            string error = "";

            try
            {
                //success = db.RegisterProductVote(userId, id, rate);
            }
            catch (System.Exception ex)
            {
                // get last error
                if (ex.InnerException != null)
                    while (ex.InnerException != null)
                        ex = ex.InnerException;

                error = ex.Message;
            }

            return Json(new { error = error, success = success, pid = id }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Rate(FormCollection form)
        {
            var rate = Convert.ToInt32(form["Score"]);
            var id = Convert.ToInt32(form["ItemID"]);
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