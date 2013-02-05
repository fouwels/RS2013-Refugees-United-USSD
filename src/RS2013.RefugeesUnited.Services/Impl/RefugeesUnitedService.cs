using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
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

		public async Task<string> GenerateUsername(string givenName, string surname)
		{
			throw new System.NotImplementedException();
		}

		public async Task<IEnumerable<RefUnitedSearchResult>> Search(string name)
		{
			throw new System.NotImplementedException();
		}
	}
}
