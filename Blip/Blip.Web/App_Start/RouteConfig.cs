using System.Web.Mvc;
using System.Web.Routing;

namespace Blip.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Login",
                "login/",
                new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
                "Register",
                "register/",
                new { controller = "Account", action = "Register" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}