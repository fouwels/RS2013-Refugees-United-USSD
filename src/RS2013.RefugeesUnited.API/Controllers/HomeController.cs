using System.Web.Mvc;
using RS2013.RefugeesUnited.Model;
using RS2013.RefugeesUnited.Services;
using RS2013.RefugeesUnited.Services.Impl;
using Tam.Lib.Model;

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
            RefugeesUnitedService testService = new RefugeesUnitedService();

            testService.GenerateUsername("Kaelan", "Fouwels");

			return Content("");
		}
	}
}
