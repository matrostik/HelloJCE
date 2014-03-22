using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HelloJCE.Models;

namespace HelloJCE.Controllers
{
    public class MoviesController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        // GET: /Movies/
        public ActionResult Index()
        {
            var m = db.Movies.Include(c => c.Comments).ToList();
            return View(m);
        }

        // GET: /Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Include(c => c.Comments).FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            MovieDetailsModel mdm = new MovieDetailsModel();
            mdm.Movie = movie;
            mdm.Comment = new Comment() { MovieId=movie.Id };

            return View(mdm);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Details()
        //{
        //}

        // GET: /Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Image,ReleaseDate,Genre,Price")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: /Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: /Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Image,ReleaseDate,Genre,Price")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: /Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: /Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult RateItem(int id, int rate)
        {
            bool success = false;
            string error = "";
            double totalRaters = 0;
            try
            {
                if (Request.Cookies["rating" + id] != null)
                    return Json(new { error = error, success = success, pid = id, total = totalRaters }, JsonRequestBehavior.AllowGet);
                Response.Cookies["rating" + id].Value = DateTime.Now.ToString();
                Response.Cookies["rating" + id].Expires = DateTime.Now.AddYears(1);

                totalRaters = IncrementRating(rate, id);
                success = true;
            }
            catch (Exception ex)
            {
                // get last error
                if (ex.InnerException != null)
                    while (ex.InnerException != null)
                        ex = ex.InnerException;

                error = ex.Message;
            }
            if (totalRaters != 0)
                success = true;
            return Json(new { error = error, success = success, pid = id, total = totalRaters }, JsonRequestBehavior.AllowGet);
        }

        public int IncrementRating(int rate, int id)
        {
            var mov = db.Movies.Where(a => a.Id == id).First();
            try
            {
                mov.Rating += rate;
                mov.TotalRaters += 1;
                db.Entry(mov).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
            return mov.TotalRaters;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment([Bind(Include = "Id,MovieId,UserName,Text")] Comment comment)
        {
            var mdm = new MovieDetailsModel();
            mdm.Movie = db.Movies.Find(comment.MovieId);
            mdm.Comment = comment;
            if (ModelState.IsValid)
            {
                try
                {
                    comment.Date = DateTime.Now;
                    Movie movie = db.Movies.Find(comment.MovieId);
                    movie.Comments.Add(comment);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return RedirectToAction("Details", new { id = comment.MovieId });
            }
            //ModelState.AddModelError("", "Some Error.");
            return View("Details", mdm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComment(int commentId)
        {
            Comment com = db.Comments.Find(commentId);
            try
            {
                db.Comments.Remove(com);
                db.SaveChanges();
            }
            catch (Exception)
            {
                
            }
            return RedirectToAction("Details", new { id = com.MovieId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
