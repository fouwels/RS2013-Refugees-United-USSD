using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using RS2013.RefugeesUnited.Model;

namespace RS2013.RefugeesUnited.Services.Impl
{
	public class RefugeesUnitedService : IRefugeesUnitedService
	{
		private readonly string _apiServerHost;
		private readonly string _apiServerUsername;
		private readonly string _apiServerPassword;

		public RefugeesUnitedService()
		{
			_apiServerHost = ConfigurationManager.AppSettings["ApiServerHost"];
			_apiServerUsername = ConfigurationManager.AppSettings["ApiServerUsername"];
			_apiServerPassword = ConfigurationManager.AppSettings["ApiServerPassword"];
		}

		public async Task<RefUnitedProfile> Register(Device device, RefUnitedProfile profile) //0%
		{
			//todo implement.
			throw new System.NotImplementedException();
		}

		public async Task<IEnumerable<RefUnitedSearchResult>> Search(RefUnitedProfile profileToSearch) // 95%
		{
			//test
			var parameters = new Dictionary<string, string>();

			if (profileToSearch.surName != null) { parameters.Add("name", profileToSearch.givenName + " " + profileToSearch.surName); }
			else if (profileToSearch.surName == null) { parameters.Add("name", profileToSearch.givenName); }
			if (profileToSearch.genderId != null) { parameters.Add("genderId", profileToSearch.genderId); }
			if (profileToSearch.birthCountryId != null) { parameters.Add("countryOfBirthId", profileToSearch.birthCountryId); }
			if (profileToSearch.lastSighting != null) { parameters.Add("lastSighting", profileToSearch.lastSighting); }
			if (profileToSearch.otherInformation != null) { parameters.Add("otherInformation", profileToSearch.otherInformation); }

			var y = await GetApi(UrlBuilder("search/", parameters)); //Raw data
			var x = JsonConvert.DeserializeObject<IEnumerable<RefUnitedSearchResult>>(y); //Processed
			return x;
		}

		public async Task<IEnumerable<RefUnitedSearchResult>> Search(string nameToSearch)  //95%
		{
			//todo test

			var parameters = new Dictionary<string, string>{{"name", nameToSearch}};

			var y = await GetApi(UrlBuilder("search/", parameters)); //Raw data
			var x = JsonConvert.DeserializeObject<IEnumerable<RefUnitedSearchResult>>(y); //Processed
			return x;
		}

		public async Task<bool> Logout(string username) //75%
		{
			//todo test
			//todo return true/false if failed/succeeded
			var y = await GetApi(UrlBuilder("profile/logout/" + username)); //raw input
			//Return true if succesfull
			return false;
		}

		public async Task<RefUnitedProfile> Login(Device device, string username, string password) //75%
		{
			//todo test
			//todo return true/false if failed/succeeded
			var parameters = new[] { new { Key = "password", Value = password } };
			var y = await GetApi(UrlBuilder("profile/login/" + username, parameters.ToDictionary(e => e.Key, e => e.Value)));

			return null;
		}

		public async Task<bool> UserExists(string username) //95%
		{
			//todo test!
			var y = await GetApi(UrlBuilder(("profile/exists/:" + username)));
			var x = JsonConvert.DeserializeAnonymousType(y, new { exists = false });
			return x.exists;
		}

		public async Task<string> GenerateUsername(string givenName, string surName) //100%
		{
			var parameters =
				new[]
					{
						new { Key = "givenName", Value = givenName },
						new { Key = "surName", Value = surName }
					};

			var y = await GetApi(UrlBuilder("usernamegenerator/", parameters.ToDictionary(e => e.Key, e => e.Value)));
			var x = JsonConvert.DeserializeAnonymousType(y, new { username = string.Empty });
			return x.username;
		}

		private async Task<string> GetApi(string url) //100%
		{
			var request = (HttpWebRequest)WebRequest.Create(url);
			request.PreAuthenticate = true;
			request.Credentials = new NetworkCredential(_apiServerUsername, _apiServerPassword);
			request.Method = "GET";
			request.ContentType = "application/json";

			var response = (HttpWebResponse) await request.GetResponseAsync();
			var responseStream = response.GetResponseStream();

			if (responseStream == null)
				return null;

			using (var sr = new StreamReader(responseStream))
				return await sr.ReadToEndAsync();
		}

		private string UrlBuilder(string apiAction, IEnumerable<KeyValuePair<string, string>> parameters) //100%
		{
			var url = (_apiServerHost + apiAction + "?");

			url += parameters.Aggregate("", (str, i) => (str
				+ HttpUtility.UrlEncode(i.Key) + "="
				+ HttpUtility.UrlEncode(i.Value) + "&"));

			return url.Substring(0, url.Length - 1);
		}

		private string UrlBuilder(string apiAction) //100%
		{
			return (_apiServerHost + apiAction);
		}

		//EG: http://api.ru.istykker.dk/usernamegenerator/?givenName=kaelan&surName=fouwels
	}
}
