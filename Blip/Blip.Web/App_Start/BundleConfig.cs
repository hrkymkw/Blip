using System.Web;
using System.Web.Optimization;


namespace Blip.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/konami").Include(
                        "~/Scripts/konami.js"));

            bundles.Add(new ScriptBundle("~/bundles/alertify").Include(
            "~/Scripts/alertify/alertify.js"));

            bundles.Add(new StyleBundle("~/bundles/alertifycss").Include("~/Content/alertify/alertify.default.css", "~/Content/alertify/alertify.core.css", "~/Content/alertify/alertify.bootstrap.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}