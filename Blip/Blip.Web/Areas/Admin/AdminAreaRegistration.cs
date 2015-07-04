using System.Web.Mvc;

namespace Blip.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_Index",
                "Admin/",
                new { controller = "Admin", action = "Index" }
            );

            context.MapRoute(
                "Admin",
                "Admin/{action}/{id}",
                new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}