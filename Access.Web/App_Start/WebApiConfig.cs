using System.Web.Http;

namespace Access.Web
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Конфигурация и службы веб-API

			// Маршруты веб-API
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				"secondID",
				"api/{controller}/{id}/{action}/{sid}",
				new {id = -1, sid = -1},
				new {id = @"^[0-9]+$"}
			);

			config.Routes.MapHttpRoute(
				"WithAction",
				"api/{controller}/{id}/{action}",
				new {id = -1},
				new {id = @"^[0-9]+$"}
			);

			config.Routes.MapHttpRoute(
				"DefaultApi",
				"api/{controller}/{id}",
				new {id = RouteParameter.Optional}
			);
		}
	}
}