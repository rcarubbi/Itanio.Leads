using System.Web.Mvc;
using System.Web.Routing;

namespace Itanio.Leads.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "DownloadArquivo",
                "Arquivo/Index/{id}",
                new {controller = "Arquivo", action = "Index", id = UrlParameter.Optional});


            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            );
        }
    }
}