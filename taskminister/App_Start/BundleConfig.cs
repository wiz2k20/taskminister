using System.Web;
using System.Web.Optimization;

namespace taskminister
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));
            
            // layout
            bundles.Add(new StyleBundle("~/layout").Include(
                "~/Content/bootstrap.css", 
                "~/Content/css/home/home.css", 
                "~/Content/css/topbar.css", 
                "~/Content/css/project.css"));

            // taskminister.musik
            bundles.Add(new ScriptBundle("~/scriptmusik").Include(
                //"~/Scripts/jquery.signalR-2.4.3.min.js",
                //"~/~/signalr/hubs",
                //"~/Content/js/musik/signalR.js",
                "~/Content/js/musik/UploadFiles.js"
                ));

            bundles.Add(new StyleBundle("~/stylemusik").Include(
                "~/Content/css/musik/style.css"));

        }
    }
}
