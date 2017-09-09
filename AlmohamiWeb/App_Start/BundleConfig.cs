using System.Web;
using System.Web.Optimization;

namespace AlmohamiWeb
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

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/GlobalPlugins").Include(
                "~/Content/font-awesome.min.css",
                "~/Content/simple-line-icons.min.css",
                "~/Content/bootstrap-rtl.min.css",
                "~/Content/bootstrap-switch-rtl.min.css"
                ));

            bundles.Add(new StyleBundle("~/Content/GlobalCss").Include(
                "~/Content/components-rtl.min.css",
                "~/Content/plugins-rtl.min.css"
               ));

            bundles.Add(new StyleBundle("~/Content/Custom").Include(
                "~/Scripts/custom.js"
               ));
            bundles.Add(new ScriptBundle("~/Bundles/CorePlugins").Include(
                    "~/Scripts/jquery.min.js",
                    "~/Scripts/bootstrap.min.js",
                    "~/Scripts/js.cookie.min.js",
                    "~/Scripts/jquery.slimscroll.min.js",
                    "~/Scripts/jquery.blockui.min.js",
                    "~/Scripts/bootstrap-switch.min.js"
                ));

            bundles.Add(new ScriptBundle("~/Bundles/GlobalScripts").Include(
                 "~/Scripts/app.min.js"
                ));

            

            bundles.Add(new ScriptBundle("~/Bundles/DashboardScripts").Include(
                 "~/Scripts/moment.min.js",
                    "~/Scripts/daterangepicker.min.js",
                    "~/Scripts/morris.min.js",
                    "~/Scripts/raphael-min.js",

                    "~/Scripts/jquery.waypoints.min.js",
                    "~/Scripts/jquery.counterup.min.js",
                     "~/Scripts/fullcalendar.min.js",
                    "~/Scripts/jquery.flot.min.js",
                    "~/Scripts/jquery.flot.resize.min.js",
                    "~/Scripts/jquery.flot.categories.min.js",
                    "~/Scripts/jquery.easypiechart.min.js",
                    "~/Scripts/jquery.sparkline.min.js",
                     "~/Scripts/jquery.vmap.js"
                     
                     ));

            //BundleTable.EnableOptimizations = true;

        }
    }
}
