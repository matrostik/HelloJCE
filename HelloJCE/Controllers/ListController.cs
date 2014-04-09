using HelloJCE.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Postal;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace HelloJCE.Controllers
{
    public class ListController : Controller
    {
        //
        // GET: /List/
        public ActionResult Index() 
        {
            ViewBag.User = User.Identity.Name;

            ApplicationDbContext dbc = new ApplicationDbContext();
            ViewBag.Roles = string.Join(",",dbc.Roles.Select(x=>x.Name).ToList<string>());
            //foreach (var r in dbc.Roles)
            //{
            //    ViewBag.Roles += r.Name + " ";
            //}
            foreach (var u in dbc.Users)
            {
                ViewBag.Users += u.UserName + "(" + string.Join(",", u.Roles.Select(r => r.Role.Name).ToList<string>()) + ") ";
            }

            ListViewModel model = new ListViewModel();
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "Swimming",
                Value = "1"
            });
            items.Add(new SelectListItem
            {
                Text = "Cycling",
                Value = "2",
                Selected = true
            });
            items.Add(new SelectListItem
            {
                Text = "Running",
                Value = "3"
            });
            model.items = items;

            ItemRating ir = new ItemRating()
            {
                ItemID = 1,
                Rating = 8,
                TotalRaters = 2,
                AverageRating = 4
            };
            model.rating = ir;
            model.item = "Cycling";

            // Host url test
            ViewBag.HostUrl = Request.Url.Host + " " + System.Web.HttpContext.Current.Request.Url.Host;
            

            return View(model);
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
            //var mov = db.Movies.Where(a => a.Id == id).First();
            //try
            //{
            //    mov.Rating += rate;
            //    mov.TotalRaters += 1;
            //    db.Entry(mov).State = EntityState.Modified;
            //    db.SaveChanges();
            //}
            //catch (Exception)
            //{
            //    return 0;
            //}
            //return mov.TotalRaters;
            return 7;
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
            var mov = db.Movies.Where(a => a.Id == id).First();
            mov.Rating += rate;
            mov.TotalRaters += 1;
            db.SaveChanges();
            var ar = new ItemRating()
            {
                ItemID = mov.Id,
                Rating = mov.Rating,
                TotalRaters = mov.TotalRaters,
                AverageRating = Convert.ToDouble(mov.Rating) / Convert.ToDouble(mov.TotalRaters)
            };
            return ar;
        }

        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string url = UploadImage(file.FileName);

                
                //string path = System.IO.Path.Combine(Server.MapPath("~/Images/profile"), pic);
                //if (!System.IO.File.Exists(Server.MapPath("~/Images/profile")))
                //{
                //    System.IO.Directory.CreateDirectory(Server.MapPath("~/Images/profile"));
                //}
                //// file is uploaded
                //file.SaveAs(path);

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                //using (MemoryStream ms = new MemoryStream())
                //{
                //    file.InputStream.CopyTo(ms);
                //    byte[] array = ms.GetBuffer();
                //}

            }
            // after successfully uploading redirect the user
            return RedirectToAction("Index");
        }

        string ClientId = "6b18f55eeee07f1";
        public string UploadImage(string image)
        {
            WebClient w = new WebClient();
            w.Headers.Add("Authorization", "Client-ID " + ClientId);
            System.Collections.Specialized.NameValueCollection Keys = new System.Collections.Specialized.NameValueCollection();
            try
            {
                Keys.Add("image", Convert.ToBase64String(System.IO.File.ReadAllBytes(image)));
                byte[] responseArray = w.UploadValues("https://api.imgur.com/3/image.xml", Keys);
                dynamic result = Encoding.ASCII.GetString(responseArray);
                XDocument xml = XDocument.Parse(result);
                var i = xml.Root.Element("link").Value;
                return i;
            }
            catch (Exception s)
            {
                //MessageBox.Show("Something went wrong. " + s.Message);
                return "Failed!";
            }
        }

    }
}