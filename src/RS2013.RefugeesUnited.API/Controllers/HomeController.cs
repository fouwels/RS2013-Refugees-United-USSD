using System.Web.Mvc;
using RS2013.RefugeesUnited.Services;

namespace RS2013.RefugeesUnited.API.Controllers
{
	public class HomeController : Controller
	{
		private ISessionService SessionService { get; set; }

		public HomeController(ISessionService sessionService)
		{
			SessionService = sessionService;
		}

		public ActionResult Index()
		{
			return Content("");
		}
	}
}
