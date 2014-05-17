using System.Web;
using System.Web.Optimization;

namespace HelloJCE
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/date").Include(
                       "~/Scripts/bootstrap-datepicker.js"));
            bundles.Add(new StyleBundle("~/Content/date").Include(
                     "~/Content/bootstrap-datepicker.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/rate").Include(
                      "~/Scripts/jquery.rating.js",
                      "~/Scripts/jquery.rating.pack.js"));
            bundles.Add(new StyleBundle("~/Content/rate").Include(
                     "~/Content/jquery.rating.css"));

            bundles.Add(new ScriptBundle("~/bundles/ifranrate").Include(
                     "~/Scripts/irfan.rating.js"));
            bundles.Add(new StyleBundle("~/Content/ifranrate").Include(
                     "~/Content/ifran.rating.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapswitch").Include(
          "~/Scripts/bootstrap-switch.js"));
            bundles.Add(new StyleBundle("~/Content/bootstrapswitch").Include(
                     "~/Content/bootstrap-switch.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstraplightbox").Include(
        "~/Scripts/bootstrap-lightbox.js"));
            bundles.Add(new StyleBundle("~/Content/bootstraplightbox").Include(
                     "~/Content/bootstrap-lightbox.css"));

            bundles.Add(new ScriptBundle("~/bundles/growl").Include(
       "~/Scripts/jquery.bootstrap-growl.js"));

            bundles.Add(new ScriptBundle("~/bundles/markdown").Include(
      "~/Scripts/bootstrap-markdown.js"));
            bundles.Add(new StyleBundle("~/Content/markdown").Include(
                     "~/Content/bootstrap-markdown.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/wysihtml5").Include(
     "~/Scripts/wysihtml5-0.3.0.js", "~/Scripts/bootstrap-wysihtml5.js"));
            bundles.Add(new StyleBundle("~/Content/wysihtml5").Include(
                     "~/Content/bootstrap-wysihtml5.css"));

            bundles.Add(new ScriptBundle("~/bundles/typeahead").Include(
    "~/Scripts/typeahead.jquery.js"));
            //bundles.Add(new StyleBundle("~/Content/typeahead").Include(
            //         "~/Content/bootstrap-wysihtml5.css"));
        }
    }
}
