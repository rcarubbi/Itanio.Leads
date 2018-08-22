using System.Web.Mvc;

namespace Itanio.Leads.WebUI.Extensions
{
    public static class NavExtensions
    {
        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null)
        {
            var cssClass = "active";
            var currentAction = (string) html.ViewContext.RouteData.Values["action"];
            var currentController = (string) html.ViewContext.RouteData.Values["controller"];

            if (string.IsNullOrEmpty(controller))
                controller = currentController;

            if (string.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ? cssClass : string.Empty;
        }
    }
}