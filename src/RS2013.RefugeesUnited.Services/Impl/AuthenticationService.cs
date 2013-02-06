using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RS2013.RefugeesUnited.Data;
using RS2013.RefugeesUnited.Model;
using RS2013.RefugeesUnited.Model.RefugeesUnited;

namespace RS2013.RefugeesUnited.Services.Impl
{
	public class AuthenticationService : IAuthenticationService
	{
		private IUserRepository UserRepository { get; set; }
		private IDeviceRepository DeviceRepository { get; set; }
		private IRefugeesUnitedService RefugeesUnitedService { get; set; }

		public AuthenticationService(IUserRepository userRepository, IDeviceRepository deviceRepository, IRefugeesUnitedService refugeesUnitedService)
		{
			if (userRepository == null) throw new ArgumentNullException("userRepository");
			if (deviceRepository == null) throw new ArgumentNullException("deviceRepository");
			if (refugeesUnitedService == null) throw new ArgumentNullException("refugeesUnitedService");

			UserRepository = userRepository;
			DeviceRepository = deviceRepository;
			RefugeesUnitedService = refugeesUnitedService;
		}

		public async Task<Profile> Authenticate(User user, Device device, string password)
		{
			return await RefugeesUnitedService.Login(device, user.RefUnitedUsername, password);
		}

		public async Task<Profile> Register(Profile user, Device device)
		{
			return await RefugeesUnitedService.Register(device, user);
		}

		public IEnumerable<User> UsersForDevice(Device device)
		{
			return UserRepository.Where(u => u.Device == device);
		}

		public void DeviceAttach(User user, Device device)
		{
			UserRepository.Change(user, (ref User u) => u.Device = device);
		}

		public void DeviceDetach(User user)
		{
			UserRepository.Change(user, (ref User u) => u.Device = null);
		}

		public void Blacklist(User user, string reason)
		{
			UserRepository.Change(user, (ref User u) => u.BlacklistReason = reason);
		}

		public void Blacklist(Device device, string reason)
		{
			DeviceRepository.Change(device, (ref Device d) => d.BlacklistReason = reason);
		}
	}
}
