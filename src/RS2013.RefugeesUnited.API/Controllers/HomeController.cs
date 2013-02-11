using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using RS2013.RefugeesUnited.Model;
using RS2013.RefugeesUnited.Model.RefugeesUnited;
using RS2013.RefugeesUnited.Services;

namespace RS2013.RefugeesUnited.API.Controllers
{
	public class HomeController : Controller
	{
		private ISessionService SessionService { get; set; }
		private IAuthenticationService AuthenticationService { get; set; }
		private IRefugeesUnitedService RefugeesUnitedService { get; set; }

		public HomeController(ISessionService sessionService, IAuthenticationService authenticationService, IRefugeesUnitedService refugeesUnitedService)
		{
			if (sessionService == null) throw new ArgumentNullException("sessionService");
			if (authenticationService == null) throw new ArgumentNullException("authenticationService");
			if (refugeesUnitedService == null) throw new ArgumentNullException("refugeesUnitedService");

			SessionService = sessionService;
			AuthenticationService = authenticationService;
			RefugeesUnitedService = refugeesUnitedService;
		}

		public async Task<ActionResult> Index()
		{
			//Test Data
			var testProfile = new Profile
			{
				username = "",
				givenName = "kaelanc",
				surName = "fouwelsc",
				password = "1234",
				otherInformation = "I like trains",
				lastSighting = "Test"
			};
			var testDevice = new Device { Number = "+447842073150" };
			//Test Data

			var testUsername = await RefugeesUnitedService.GenerateUsername(testProfile.givenName, testProfile.surName);
			testProfile.username = testUsername;
			//Get API to generate a Username

			var testUserExists = await RefugeesUnitedService.UserExists(testProfile.username);
			//Check if user exists

			//var testRegister = await RefugeesUnitedService.Register(testDevice, testProfile); // todo <-- Problematic
			//Attempt to register said user

			var testLogin = await RefugeesUnitedService.Login(testDevice, "kaelanc.fouwelsc", "1234");
			//Atempt to login said user

			var testSearch = await RefugeesUnitedService.Search(testProfile);
			//Search for said user

			var testLogout = await RefugeesUnitedService.Logout("kaelanc.fouwelsc");
			//Attempt to logout said user

			//Break here
			return Content("");
		}

		[HttpPost]
		public async Task<ActionResult> GlobalUSSD(
			string subscriber, string protocol,
			string sadsSmsMessage = null, string sadsListSegment = null,
			string sadsTextSegment = null, string sadsErrorMessage = null)
		{
			var connector = Request.Headers["X-Connector"]; // wap/sip/http/smpp
			var connectorDescription = Request.Headers["X-Connector-Description"];
			var userAgent = Request.UserAgent;
			var host = Request.Headers["Host"];
			var contentType = Request.ContentType;
			var ussdMessage = Request.Headers["WHOISD-USSD-MESSAGE"]; // USSD user reply
			var ussdAddress = Request.Headers["WHOISD-USSD-ADDRESS"];
			var cookies = Request.Cookies; // To identify session
			var locationCountry = Request.Headers["SADS-Location-Country-Name"];
			var locationCity = Request.Headers["SADS-Location-City-Name"];
			var locationCountryId = Request.Headers["SADS-Location-Country-Id"];
			var locationCityId = Request.Headers["SADS-Location-City-Id"];
			var locationVLR = Request.Headers["SADS-Location-VLR"];
			var locationLatitude = Request.Headers["SADS-Location-Latitude"];
			var locationLongitude = Request.Headers["SADS-Location-Longitude"];
			var locationTimezone = Request.Headers["SADS-Location-Timezone"];

			var response = "";

			try
			{
				Session session = null;
			
				if (cookies["session"] != null)
				{
					long sessionId;

					if (long.TryParse(cookies["session"].Value, out sessionId))
						session = SessionService.RetrieveSession(sessionId);
				}

				if (session == null)
				{
					session = SessionService.CreateSession(subscriber);
					SessionService.SetSessionState(session, SessionState.AuthenticationOptions);

					response = "1) Log in\n" +
					           "2) Register\n" +
					           "3) Connect account";
				}
				else
				{
					switch (session.State)
					{
						case SessionState.AuthenticationOptions:
							switch (ussdMessage)
							{
								case "1":
									var users = AuthenticationService.UsersForDevice(session.Device);
									SessionService.SetSessionState(session, SessionState.Login, JsonConvert.SerializeObject(users.Select(u => u.Id)));

									var i = 0;
									response = users.Aggregate("Choose an account:", (s, u) => s + string.Format("\n{0}) {1}, {2}", i++, u.Initials, DateTime.Now.Year - u.DateOfBirth.Year));
									break;

								case "2":
									SessionService.SetSessionState(session, SessionState.Register);
									break;

								case "3":
									SessionService.SetSessionState(session, SessionState.ConnectAccount);
									break;

								default:
									throw new UnauthorizedAccessException();
							}
							break;

						case SessionState.ConnectAccount:
							response =
								"[Connect Account]\n)";
							break;
						case SessionState.Register:
							response =
								"[Register]\n)";
							break;
						case SessionState.Login:
							response =
								"[Login]\nEnter choice\n1)Login with login code\n2) Connect account - choose this if first login from device";
							break;
						case SessionState.LoginCode:
							response =
								"Enter your login code";
							break;

						case SessionState.MainMenu:
							response = 
								"[Main Menu]\nEnter choice\n1) Login\n2) Register\n3) Connect Account";
							break;

						default:
							SessionService.TerminateSession(session);
							throw new UnauthorizedAccessException();
					}
				}
			}
			catch (UnauthorizedAccessException)
			{
				response = "Your session has been terminated. Please initiate a new session to continue.";
			}

			return Content(response);
		}
	}
}
