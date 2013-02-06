using System.Threading.Tasks;
using RS2013.RefugeesUnited.Model;
using RS2013.RefugeesUnited.Model.RefugeesUnited;

namespace RS2013.RefugeesUnited.Services
{
	public interface IAuthenticationService
	{
		/// <summary>
		/// Attempts to authenticate a user based on the given 
		/// </summary>
		/// <param name="user">User to authenticate</param>
		/// <param name="device">Device request came from</param>
		/// <param name="password">Password given by user</param>
		/// <returns></returns>
		Task<Profile> Authenticate(User user, Device device, string password);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="user"></param>
		/// <param name="device"></param>
		/// <returns></returns>
		Task<User> Register(Profile user, Device device);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="user"></param>
		/// <param name="device"></param>
		void DeviceAttach(User user, Device device);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="user"></param>
		void DeviceDetach(User user);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="user"></param>
		/// <param name="reason"></param>
		void Blacklist(User user, string reason);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="device"></param>
		/// <param name="reason"></param>
		void Blacklist(Device device, string reason);
	}
}
