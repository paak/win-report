using System.Web.Optimization;

namespace WINConnect.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;

            AddCss(bundles);
            AddJavaScript(bundles);
        }

        private static void AddCss(BundleCollection bundles)
        {
            // Bootstrap - Twitter Bootstrap CSS (http://getbootstrap.com/).
            // Site - Your custom site CSS.
            // Note: No CDN support has been added here. Most likely you will want to customize your copy of bootstrap.
            bundles.Add(new StyleBundle(
                "~/Content/css")
                .Include("~/Content/bootstrap/site.css")
                .Include("~/Content/site.css"));

            // Font Awesome - Icons using font (http://fortawesome.github.io/Font-Awesome/).
            bundles.Add(new StyleBundle(
                "~/Content/fa")
                .Include("~/Content/fontawesome/site.css"));

            // jQuery UI
            bundles.Add(new StyleBundle(
                "~/Content/jqueryui")
                .Include("~/Content/jquery-ui-1.11.4.css"));
        }

        private static void AddJavaScript(BundleCollection bundles)
        {
            // jQuery - The JavaScript helper API (http://jquery.com/).
            Bundle jqueryBundle = new ScriptBundle("~/scripts/jquery")
                .Include("~/Scripts/jquery-{version}.js");
            bundles.Add(jqueryBundle);

            // jQuery Validate - Client side JavaScript form validation (http://jqueryvalidation.org/).
            Bundle jqueryValidateBundle = new ScriptBundle(
                "~/scripts/jqueryval")
                .Include("~/Scripts/jquery.unobtrusive*")
                .Include("~/Scripts/jquery.validate*");
            bundles.Add(jqueryValidateBundle);

            //jQuery UI
            Bundle jqueryUIBundle = new ScriptBundle(
                "~/scripts/jqueryui")
                .Include("~/Scripts/jquery-ui-1.11.4.js");
            bundles.Add(jqueryUIBundle);

            // Bootstrap - Twitter Bootstrap JavaScript (http://getbootstrap.com/).
            Bundle bootstrapBundle = new ScriptBundle(
                "~/scripts/bootstrap")
                .Include("~/Scripts/bootstrap*");
            bundles.Add(bootstrapBundle);

            //App
            Bundle win = new ScriptBundle(
                "~/scripts/winapp")
                .Include("~/Scripts/app/win.js");
            bundles.Add(win);

            // Modernizr - Allows you to check if a particular API is available in the browser (http://modernizr.com).
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            // Note: The current version of Modernizr does not support Content Security Policy (CSP) (See FilterConfig).
            // See here for details: https://github.com/Modernizr/Modernizr/pull/1263 and 
            // http://stackoverflow.com/questions/26532234/modernizr-causes-content-security-policy-csp-violation-errors
            Bundle modernizrBundle = new ScriptBundle(
                "~/scripts/modernizr")
                .Include("~/Scripts/modernizr-*");
            bundles.Add(modernizrBundle);
        }
    }
}