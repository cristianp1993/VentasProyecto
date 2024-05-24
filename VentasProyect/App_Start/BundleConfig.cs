using System.Web;
using System.Web.Optimization;

namespace VentasProyect
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Login").Include(
                "~/Content/Login/Login.css"));

            bundles.Add(new StyleBundle("~/Layout").Include(
                "~/Content/Layout/Layout.css"));

            bundles.Add(new ScriptBundle("~/ScrLayout").Include(
                "~/Scripts/Layout/Script.js"));

            bundles.Add(new StyleBundle("~/HomeIndex").Include(
                "~/Content/Home/index.css"));

            bundles.Add(new StyleBundle("~/tables").Include(
                "~/Content/Generales/table.css"));

            bundles.Add(new StyleBundle("~/Forms").Include(
                "~/Content/Generales/Form.css"));

            bundles.Add(new ScriptBundle("~/TableScript").Include(
                "~/Scripts/Tables.js"));

            bundles.Add(new ScriptBundle("~/ScCreate").Include(
                "~/Scripts/Personas/Create.js"));

            bundles.Add(new StyleBundle("~/Validations").Include(
                "~/Content/Generales/Validation.css"));

            // Agregar CryptoJS desde los archivos descargados
            bundles.Add(new ScriptBundle("~/bundles/cryptojs").Include(
                "~/Scripts/CryptoJS/core.js",
                "~/Scripts/CryptoJS/sha256.js"));

            bundles.Add(new ScriptBundle("~/ScProduct").Include(
                "~/Scripts/Productos/Productos.js"));

            bundles.Add(new StyleBundle("~/ProductStyle").Include(
                "~/Content/Productos/Productos.css"));


            bundles.Add(new StyleBundle("~/Popup").Include(
                "~/Content/Home/popup.css"));

            bundles.Add(new ScriptBundle("~/ScriptBtFilter").Include(
                "~/Scripts/Home/index.js"));

        }
    }
}
