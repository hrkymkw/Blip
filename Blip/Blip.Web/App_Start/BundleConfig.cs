using System.Web;
using System.Web.Optimization;


namespace Blip.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            bundles.Add(new ScriptBundle("~/bundles/mainjs").Include(
             "~/Scripts/bootstrap.min.js", "~/Scripts/jquery-1.10.2.min.js", "~/Scripts/alertify/alertify.js",
             "~/Scripts/modernizr-2.6.2.js", "~/Scripts/jquery-ui-1.11.4.min.js", "~/Scripts/jquery.validate.min.js", "~/Scripts/jquery.validate.unobtrusive.min.js",
             "~/Scripts/konami.js", "~/Scripts/Layout.js"));

            bundles.Add(new StyleBundle("~/bundles/maincss").Include("~/Content/Site.css",
            "~/Content/bootstrap.min.css", "~/Content/themes/base/jquery-ui.css",
            "~/Content/alertify/alertify.default.css", "~/Content/alertify/alertify.core.css",
             "~/Content/alertify/alertify.bootstrap.css", "~/Content/css/font-awesome.css"));
        }
    }
}