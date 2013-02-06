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
			//var testUsername = await RefugeesUnitedService.GenerateUsername("Kaelan", "Fouwels");
			//var testUserExists = await RefugeesUnitedService.UserExists("kaelan.fouwels");
			//var testLogin = await RefugeesUnitedService.Login(null, "kaelan.fouwels", "8740");
			//var testLogout = await RefugeesUnitedService.Logout("kaelanb.fouwels");

			var testProfile = new Profile
			{
				givenName = "kaelan",
				surName = "fouwels",
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
