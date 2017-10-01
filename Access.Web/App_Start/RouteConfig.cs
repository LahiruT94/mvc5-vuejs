using System.Web.Mvc;
using System.Web.Routing;

namespace Access.Web
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute("single", "{controller}/{id}",
				new {controller = "Home", action = "Index"},
				new {id = @"^[0-9]+$"});

			routes.MapRoute(
				"Default",
				"{controller}/{action}/{id}",
				new {controller = "Home", action = "Index", id = UrlParameter.Optional}
			);
		}
	}
}