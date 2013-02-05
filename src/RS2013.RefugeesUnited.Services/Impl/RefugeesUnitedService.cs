using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RS2013.RefugeesUnited.Model;
using System.Net;

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
			//todo implement.
            throw new System.NotImplementedException();
        }

		public async Task<IEnumerable<RefUnitedSearchResult>> Search(RefUnitedProfile profileToSearch) //WIP 50%
		{
			//todo Convert Json into refUnitedProfile obj.
			//todo make async
			List<string> parameters = new List<string>();

			if (profileToSearch.surName != null)			{ parameters.Add("name=" + profileToSearch.givenName + " " + profileToSearch.surName);}
			else if (profileToSearch.surName == null)		{ parameters.Add("name=" + profileToSearch.givenName);}
			if (profileToSearch.genderId != null)			{ parameters.Add("genderId=" + profileToSearch.genderId);}
			if (profileToSearch.birthCountryId != null)		{ parameters.Add("countryOfBirthId=" + profileToSearch.birthCountryId); }
			if (profileToSearch.lastSighting != null)		{ parameters.Add("lastSighting=" + profileToSearch.lastSighting); }
			if (profileToSearch.otherInformation != null)	{ parameters.Add("otherInformation=" + profileToSearch.otherInformation); }
			
			var y = GetApi(UrlBuilder("search/", parameters)); //Raw data
			//var x = JsonConvert.DeserializeObject(y);

			throw new System.NotImplementedException();
		}

		public async Task<IEnumerable<RefUnitedSearchResult>> Search(string nameToSearch)  //0%
		{
			//todo implment.
			throw new System.NotImplementedException();
		}

        public async Task<bool> Logout(string username) //75%
        {
			//todo make async
			//todo test
			//todo return true/false if failed/succeeded
	        var y = GetApi(UrlBuilder("profile/logout/" + username)); //raw input
			//Return true if succesfull
	        return false;
        }

		public async Task<bool> Login(string username, string password) //75%
        {
			//todo make async
			//todo test
			//todo return true/false if failed/succeeded
			List<string> parameters = new List<string> {"password=" + password};
			var y = GetApi(UrlBuilder("profile/login/" + username, parameters)); //raw input

            //Return true if successfull
	        return false;
        } 

        public async Task<bool> UserExists(string username) //95%
        {
			//todo test!
			//todo make async
	        var y = GetApi(UrlBuilder(("profile/exists/:" + username))); //raw input
	        var x = JsonConvert.DeserializeAnonymousType(y, new {exists = false});
	        return x.exists;
        } 
		
	    public async Task<string> GenerateUsername(string givenName, string surName) //100%
	    {
		    List<string> parameters = new List<string> {"givenName=" + givenName, "surName=" + surName};

		    var y = GetApi(UrlBuilder("usernamegenerator/", parameters)); //rwa input
		    var x = JsonConvert.DeserializeAnonymousType(y, new {username = string.Empty});
		    return x.username;
	    }

        private string GetApi(string url) //95%
        {
            //todo: Make Async
            //todo: Error handling << What if 404?

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.PreAuthenticate = true;
            request.Credentials = new NetworkCredential(_apiServerUsername, _apiServerPassword);
            request.Method = "GET";
            request.ContentType = "application/json";

			HttpWebResponse response = (HttpWebResponse) request.GetResponse(); //Working.

            StreamReader sr0 = new StreamReader(response.GetResponseStream());

			string tempString = sr0.ReadToEnd();

            response.Close();
            sr0.Close();

            return tempString;
        }

        private string UrlBuilder(string apiAction, List<string> parameters) //100%
        {
            string url = (_apiServerHost + apiAction + "?");
            return parameters.Aggregate(url, (current, parameter) => (current + parameter + "&"));
        }

		private string UrlBuilder(string apiAction) //100%
		{
			return(_apiServerHost + apiAction);
		}

        //EG: http://api.ru.istykker.dk/usernamegenerator/?givenName=kaelan&surName=fouwels
    }
}
