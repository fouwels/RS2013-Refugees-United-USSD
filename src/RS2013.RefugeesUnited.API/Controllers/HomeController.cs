using System.Threading.Tasks;
using System.Web.Mvc;
using RS2013.RefugeesUnited.Model;
using RS2013.RefugeesUnited.Model.RefugeesUnited;
using RS2013.RefugeesUnited.Services;
using RS2013.RefugeesUnited.Model;

namespace RS2013.RefugeesUnited.API.Controllers
{
	public class HomeController : Controller
	{
		private ISessionService SessionService { get; set; }
		private IRefugeesUnitedService RefugeesUnitedService { get; set; }

		public HomeController(ISessionService sessionService, IRefugeesUnitedService refugeesUnitedService)
		{
			SessionService = sessionService;
			RefugeesUnitedService = refugeesUnitedService;
		}

		public async Task<ActionResult> Index()
		{
			//var testUsername = await RefugeesUnitedService.GenerateUsername("Kaelanc", "Fouwelsc");
			//var testUserExists = await RefugeesUnitedService.UserExists("kaelanc.fouwelsc");
			//var testLogin = await RefugeesUnitedService.Login(null, "kaelanc.fouwelsc", "1234");
			//var testLogout = await RefugeesUnitedService.Logout("kaelanc.fouwelsc");

			var testProfile = new Profile
			{
				username = "kaelanc.fouwelsc",
				givenName = "kaelanc",
				surName = "fouwelsc",
				password = "1234",
				otherInformation = "I like trains",
				lastSighting = "Test"
			};

			//var testSearch = await RefugeesUnitedService.Search(testProfile);

			var testDevice = new Device {Number = "+447842073150"};
			var testRegister = await RefugeesUnitedService.Register(testDevice, testProfile);

			return Content("");
		}
	}
}
