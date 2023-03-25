using System.Web;
using System.Web.Optimization;

namespace MS.WebSite
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            ////USER AREA
            bundles.Add(new StyleBundle("~/Content/css/userArea").Include(
                    "~/Content/bootstrap.css",
                    "~/Content/font-awesome.min.css",
                    "~/Content/style_user-area.css",
                    //"~/Content/jquery-ui.min.css",
                    "~/Content/bootstrap-datepicker.min.css",
                    "~/Content/jquery.timepicker.min.css"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/userArea").Include(
                    "~/Scripts/jquery-3.1.1.js",
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/respond.js",
                    "~/Scripts/modernizr-2.8.3.js",
                    //"~/Scripts/jquery-ui.min.js",
                    "~/Scripts/bootstrap-datepicker.min.js",
                    "~/Scripts/jquery.timepicker.min.js"
                    ));
            ////

            bundles.Add(new StyleBundle("~/Content/css/main").Include(
                    "~/Content/bootstrap.css",
                    "~/Content/magnific-popup.css",
                    "~/Content/style.css"));
            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/jquery-3.1.1.js"));
        }
    }
}
