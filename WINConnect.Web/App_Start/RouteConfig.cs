using System.Web.Mvc;
using System.Web.Routing;

namespace WINConnect.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.AppendTrailingSlash = true;
            routes.LowercaseUrls = true;

            // IgnoreRoute - Tell the routing system to ignore certain routes for better performance.
            // Ignore .axd files.
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // Ignore everything in the Content folder.
            routes.IgnoreRoute("Content/{*pathInfo}");
            // Ignore everything in the Scripts folder.
            routes.IgnoreRoute("Scripts/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Agent", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}