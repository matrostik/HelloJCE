using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloJCE.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Hello JCE description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Hello JCE contact page.";

            return View();
        }
    }
}