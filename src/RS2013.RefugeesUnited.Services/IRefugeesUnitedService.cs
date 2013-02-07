using System.Collections.Generic;
using System.Threading.Tasks;
using RS2013.RefugeesUnited.Model;
using RS2013.RefugeesUnited.Model.RefugeesUnited;

namespace RS2013.RefugeesUnited.Services
{
	public interface IRefugeesUnitedService
	{
		/// <summary>
		/// Attempts to login into a Refugees United profile
		/// </summary>
		/// <param name="device">Device request was made from</param>
		/// <param name="username">Username for user</param>
		/// <param name="password">Password for user</param>
		/// <returns>A profile object, or null if the authentication failed</returns>
		Task<Profile> Login(Device device, string username, string password);

		/// <summary>
		/// Attempts to logout a user's Refugees United profile
		/// </summary>
		/// <param name="username">Username for user</param>
		/// <returns>Indicates whether logout succeeded</returns>
		Task<bool> Logout(string username);
		
		/// <summary>
		/// Attempts to register the profile with Refugees United
		/// </summary>
		/// <param name="device">Device used to make the registration</param>
		/// <param name="profile">Profile to create</param>
		/// <returns>Created profile</returns>
		Task<Profile> Register(Device device, Profile profile);
		
		/// <summary>
		/// Finds out if a username is valid & exists in the Refugees United system
		/// </summary>
		/// <param name="username">Username to check</param>
		/// <returns>Indicates whether username exists</returns>
		Task<bool> UserExists(string username);

		/// <summary>
		/// Gets users profile from profile ID
		/// </summary>
		/// <param name="profileId">Profile ID to lookup</param>
		/// <returns>Profile of user</returns>
		Task<Profile> GetProfile(string profileId);

		/// <summary>
		/// Generates available username based on name given
		/// </summary>
		/// <param name="givenName">Given/first name of user</param>
		/// <param name="surname">Surname of user</param>
		/// <returns>Available username</returns>
		Task<string> GenerateUsername(string givenName, string surname);

		/// <summary>
		/// Searches for other profiles
		/// </summary>
		/// <param name="profile">Profile object of person being searched for</param>
		/// <returns>Enumerable list of search results</returns>
		Task<IEnumerable<SearchResult>> Search(Profile profile);

		/// <summary>
		/// Searches for other profiles
		/// </summary>
		/// <param name="name">Name of person being searched for</param>
		/// <returns>Enumerable list of search results</returns>
		Task<IEnumerable<SearchResult>> Search(string name);
	}
}
