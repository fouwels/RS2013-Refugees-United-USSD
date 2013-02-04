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
            //TESTING
            const string apiServerHost = "http://api.ru.istykker.dk/";
            const string apiServerUsername = "hackathon";
            const string apiServerPassword = "179d50c6eb31188925926a5d1872e8117dc58572";
            
            RefUnitedApiConnect testApiConnector = new RefUnitedApiConnect(
                                                                            apiServerHost, 
                                                                            apiServerUsername, 
                                                                            apiServerPassword);
		    User testUser = new User();
		    
            testApiConnector.ProfileGet(testUser);

            //---------------------
			return Content("");
		}
	}
}
