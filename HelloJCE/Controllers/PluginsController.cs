using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloJCE.Controllers
{
    public class PluginsController : Controller
    {
        //Bootstrap Switch
        // GET: /Plugins/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult BootstrapSwitch()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BootstrapSwitch(bool isOn)
        {
            return View();
        }

        public ActionResult BootstrapLightbox()
        {
            return View();
        }

        public ActionResult BootstrapGrowl()
        {
            return View();
        }

        public ActionResult BootstrapMarkdown()
        {
            return View();
        }

        public ActionResult BootstrapWYSIHTML5()
        {
            return View();
        }

        public ActionResult Typeahead()
        {
            return View();
        }
	}
}