using System.Web.Optimization;

namespace Itanio.Leads.WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/jquery.validate.custom.pt-br.js",
                "~/Scripts/plugins/mask/jquery.mask.js",
                "~/Scripts/plugins/maskMoney/jquery.maskMoney.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/animate.css",
                "~/Content/Site.css",
                "~/Content/style.css"));

            // Font Awesome icons
            bundles.Add(new StyleBundle("~/font-awesome/css").Include(
                "~/Content/font-awesome.min.css", new CssRewriteUrlTransform()));


            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/plugins/metisMenu/metisMenu.min.js",
                "~/Scripts/plugins/pace/pace.min.js",
                "~/Scripts/app/inspinia.js",
                "~/Scripts/app/app.js"));

            // iCheck css styles
            bundles.Add(new StyleBundle("~/Content/plugins/iCheck/iCheckStyles").Include(
                "~/Content/plugins/iCheck/custom.css"));

            // iCheck
            bundles.Add(new ScriptBundle("~/plugins/iCheck").Include(
                "~/Scripts/plugins/iCheck/icheck.js"));

            // dataTables styles
            bundles.Add(new StyleBundle("~/Content/plugins/dataTables/dataTablesStyles").Include(
                "~/Content/plugins/dataTables/dataTables.bootstrap.css",
                "~/Content/plugins/dataTables/dataTables.responsive.css",
                "~/Content/plugins/dataTables/dataTables.tableTools.min.css"));

            // dataTables 
            bundles.Add(new ScriptBundle("~/plugins/dataTables").Include(
                "~/Scripts/plugins/dataTables/jquery.dataTables.js",
                "~/Scripts/plugins/dataTables/dataTables.bootstrap.js",
                "~/Scripts/plugins/dataTables/dataTables.responsive.js",
                "~/Scripts/plugins/dataTables/dataTables.tableTools.min.js",
                "~/Scripts/plugins/dataTables/dataTables.pipeline.js"));

            // SlimScroll
            bundles.Add(new ScriptBundle("~/plugins/slimScroll").Include(
                "~/Scripts/plugins/slimscroll/jquery.slimscroll.min.js"));

            // toastr notification 
            bundles.Add(new ScriptBundle("~/plugins/toastr").Include(
                "~/Scripts/plugins/toastr/toastr.min.js"));

            // toastr notification styles
            bundles.Add(new StyleBundle("~/plugins/toastrStyles").Include(
                "~/Content/plugins/toastr/toastr.min.css"));
        }
    }
}