using Blip.Web.Filters;
using System.Web.Mvc;

namespace Blip.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LogFilterAttribute());
        }
    }
}