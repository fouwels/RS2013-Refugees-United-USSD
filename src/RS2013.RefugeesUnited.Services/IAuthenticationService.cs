using RS2013.RefugeesUnited.Model;

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
		User Authenticate(User user, Device device, string password);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="user"></param>
		/// <param name="device"></param>
		/// <returns></returns>
		User Register(User user, Device device);

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
		void Blacklist(User user);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="device"></param>
		void Blacklist(Device device);
	}
}
