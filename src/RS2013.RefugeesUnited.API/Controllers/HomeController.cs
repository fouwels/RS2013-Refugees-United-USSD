using System.Threading.Tasks;
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
			IRefugeesUnitedService testService = new RefugeesUnitedService();

			//var testUsername = testService.GenerateUsername("Kaelan", "Fouwels");
			//var testUserExists = testService.UserExists("kaelan.fouwels");
			//var testLogin = testService.Login("kaelan.fouwels", "8740");
			//var testLogout = testService.Logout("kaelanb.fouwels");

			//RefUnitedProfile testProfile = new RefUnitedProfile
			//	{
			//		givenName = "kaelan",
			//		surName = "fouwels",
			//		otherInformation = "I like trains",
			//		lastSighting = "Test"
			//	};

			//var testSearch = testService.Search(testProfile);

			return Content("");
		}
	}
}
