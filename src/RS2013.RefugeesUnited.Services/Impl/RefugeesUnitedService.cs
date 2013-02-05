using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<RefUnitedProfile> Login(Device device, string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Logout(string username)
        {
            throw new System.NotImplementedException();
        }

        public async Task<RefUnitedProfile> Register(Device device, RefUnitedProfile profile)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UserExists(string username)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<RefUnitedSearchResult>> Search(string name)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> GenerateUsername(string givenName, string surName) //WIP
        {
            //done: Extract username from api response
            //done: Return username as string
            //todo: Error handling << What if null?

            List<string> parameters = new List<string> {"givenName=" + givenName, "surName=" + surName};

            //EG:In as  "\n{\"username\":\"kaelan.fouwels\"}"
            return GetApi(UrlBuilder("usernamegenerator/", parameters)).Split('"')[3];
        }


        private string GetApi(string url) //WIP
        {
            //done: Return API data as x << done
            //todo: Async shizzle.
            //todo: Error handling << What if 404?

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.PreAuthenticate = true;
            request.Credentials = new NetworkCredential(_apiServerUsername, _apiServerPassword);
            request.Method = "GET";
            request.ContentType = "application/json";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse(); //Working.

            StreamReader sr0 = new StreamReader(response.GetResponseStream());
            string tempString = sr0.ReadToEnd();

            response.Close();
            sr0.Close();

            return tempString;
        }

        private string UrlBuilder(string apiAction, List<string> parameters) //Working
        {
            string url = (_apiServerHost + apiAction + "?");
            return parameters.Aggregate(url, (current, parameter) => (current + parameter + "&"));
        }

        //EG: http://api.ru.istykker.dk/usernamegenerator/?givenName=kaelan&surName=fouwels
    }
}
