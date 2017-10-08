using System.Web.Mvc;

namespace Access.Controllers
{
	public class HomeController : Controller
	{
		// GET: Home
		public ActionResult Index()
		{
			return View();
		}
	}
}