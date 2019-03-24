using System.Web;
using System.Web.Optimization;

namespace VendasMVC
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
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/popper").Include(
                      "~/Scripts/Custom/popper.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/template").Include(
                      "~/Scripts/jquery.easing.min.js",
                      "~/Scripts/Custom/sb-admin-2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/validateCpf").Include(
                      "~/Scripts/Custom/validate-cpf.js"));

            bundles.Add(new ScriptBundle("~/bundles/validateValor").Include(
                      "~/Scripts/Custom/validate-valor.js"));

            bundles.Add(new ScriptBundle("~/bundles/funcionalidades").Include(
                      "~/Scripts/Custom/funcionalidades.js"));

            bundles.Add(new ScriptBundle("~/bundles/manipulaVenda").Include(
                      "~/Scripts/Custom/manipula-venda.js"));

            bundles.Add(new ScriptBundle("~/bundles/deleteButton").Include(
                      "~/Scripts/Custom/delete-button.js"));

            bundles.Add(new ScriptBundle("~/bundles/chart").Include(
                      "~/Scripts/Custom/Chart/Chart.min.js",
                      "~/Scripts/Custom/Chart/pie-chart.js",
                      "~/Scripts/Custom/Chart/area-chart.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                      "~/Scripts/Custom/DataTables/jquery.dataTables.min.js",
                      "~/Scripts/Custom/DataTables/dataTables.bootstrap4.min.js",
                      "~/Scripts/Custom/DataTables/dataTablesInit.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Custom/site.css"));

            bundles.Add(new StyleBundle("~/Content/template").Include(
                      "~/Content/Custom/sb-admin-2.min.css"));

            bundles.Add(new StyleBundle("~/Content/datatables").Include(
                      "~/Content/Custom/DataTables/dataTables.bootstrap4.min.css"));
        }
    }
}
