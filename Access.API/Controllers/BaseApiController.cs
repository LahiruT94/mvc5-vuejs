using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using Newtonsoft.Json;

namespace Access.API
{
	[EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
	public abstract class BaseApiController : ApiController
	{
		protected internal new JsonResult<T> Json<T>(T content)
		{
			var settings = new JsonSerializerSettings
			{
				Re‌​ferenceLoopHandling = ReferenceLoopHandling.Ignore
			};

			return base.Json(content, settings);
		}
	}
}