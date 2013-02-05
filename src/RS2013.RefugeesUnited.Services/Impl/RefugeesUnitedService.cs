using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

		public async Task<RefUnitedProfile> Register(Device device, RefUnitedProfile profile)
		{
			throw new System.NotImplementedException();
		}

		public async Task<IEnumerable<RefUnitedSearchResult>> Search(RefUnitedProfile profileToSearch) //WIP 50%
		{
			var parameters = new List<string>();

			if (profileToSearch.surName != null) { parameters.Add("name=" + profileToSearch.givenName + " " + profileToSearch.surName); }
			else if (profileToSearch.surName == null) { parameters.Add("name=" + profileToSearch.givenName); }
			if (profileToSearch.genderId != null) { parameters.Add("genderId=" + profileToSearch.genderId); }
			if (profileToSearch.birthCountryId != null) { parameters.Add("countryOfBirthId=" + profileToSearch.birthCountryId); }
			if (profileToSearch.lastSighting != null) { parameters.Add("lastSighting=" + profileToSearch.lastSighting); }
			if (profileToSearch.otherInformation != null) { parameters.Add("otherInformation=" + profileToSearch.otherInformation); }

			var y = await GetApi(UrlBuilder("search/", parameters)); //Raw data

			throw new System.NotImplementedException();
		}

		public async Task<IEnumerable<RefUnitedSearchResult>> Search(string nameToSearch)
		{
			throw new System.NotImplementedException();
		}

		public async Task<bool> Logout(string username)
		{
			var y = await GetApi(UrlBuilder("profile/logout/" + username));

			//Return true if succesfull
			return false;
		}

		public async Task<bool> Login(string username, string password)
		{
			var parameters = new List<string> { "password=" + password };
			var y = await GetApi(UrlBuilder("profile/login/" + username, parameters));

			//Return true if successfull
			return false;
		}

		public async Task<bool> UserExists(string username)
		{
			var y = await GetApi(UrlBuilder(("profile/exists/:" + username)));
			var x = JsonConvert.DeserializeAnonymousType(y, new { exists = false });
			return x.exists;
		}

		public async Task<string> GenerateUsername(string givenName, string surName) //95%
		{
			//done: Extract username from api response
			//done: Return username as string
			//todo: Error handling << What if null?

			var parameters = new List<string> { "givenName=" + givenName, "surName=" + surName };

			var y = await GetApi(UrlBuilder("usernamegenerator/", parameters)); //rwa input
			var x = JsonConvert.DeserializeAnonymousType(y, new { username = string.Empty });
			return x.username;
		}

		private async Task<string> GetApi(string url)
		{
			//done: Return API data as x << done
			//todo: Error handling << What if 404?

			var request = (HttpWebRequest)WebRequest.Create(url);
			request.PreAuthenticate = true;
			request.Credentials = new NetworkCredential(_apiServerUsername, _apiServerPassword);
			request.Method = "GET";
			request.ContentType = "application/json";

			var response = (HttpWebResponse)request.GetResponse();
			var responseStream = response.GetResponseStream();

			if (responseStream == null)
				return null;

			using (var sr = new StreamReader(responseStream))
				return await sr.ReadToEndAsync();
		}

		private string UrlBuilder(string apiAction, IEnumerable<string> parameters)
		{
			var url = (_apiServerHost + apiAction + "?");
			return parameters.Aggregate(url, (current, parameter) => (current + parameter + "&"));
		}

		private string UrlBuilder(string apiAction)
		{
			return (_apiServerHost + apiAction);
		}

		//EG: http://api.ru.istykker.dk/usernamegenerator/?givenName=kaelan&surName=fouwels
	}
}
