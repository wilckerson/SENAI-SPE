using System.Web;
using System.Web.Optimization;

namespace SPE
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Enable application to force use Bundling in Debug-Mode
            BundleTable.EnableOptimizations = true;

            #region Bundles Scripts
            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-1.10.3.custom.min.js",
                //"~/Scripts/jqueryui.js",
                        "~/Scripts/base.SPE.js",
                        "~/Scripts/Validacoes/base.validacao.SPE.js",
                         //"~/Content/bootstrap/js/bootstrap.min.js",
                          "~/Scripts/maskInput.js",
                          "~/Scripts/jQuery.dPassword.min.js",
                          //"~/Scripts/Matriz/matriz.js",
                //"~/Scripts/bootstrap-datepicker.js",
                         "~/Scripts/jquery.maskMoney.js",
                          "~/Scripts/xdate.js",
                //"~/Scripts/bootstrap-timepicker.min.js",  
                         "~/Scripts/sort.js",
                         "~/Scripts/Turma/turma.js",
                         "~/Scripts/jquery-ui-timepicker-addon.js",
                         "~/Scripts/accentRemove.js",
                         "~/Scripts/jquery.dataTables.Custom.js"
                         //"~/Scripts/dataTableCustom.js"
                        ));

            bundles.Add(new ScriptBundle("~/Scripts/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/Scripts/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/methods_pt.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/Scripts/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            #endregion

            #region Bundles Styles
            bundles.Add(new StyleBundle("~/Style/css").Include(
                //"~/Content/css/jquery-ui-1.10.3.custom.min.css",
                //"~/Content/bootstrap/css/bootstrap.min.css",
                "~/Content/bootstrap/css/bootstrap-responsive.min.css",
                "~/Content/css/estilo.css",
                "~/Content/css/normalize.css",     
                "~/Content/bootstrap/css/jquery-ui.css",
                //"~/Content/css/bootstrap-timepicker.min.css",
                   "~/Content/css/jquery-ui-timepicker-addon.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"

                       ));
            #endregion
        }
    }
}