using System;
using System.Linq;
using RS2013.RefugeesUnited.Data;
using RS2013.RefugeesUnited.Model;

namespace RS2013.RefugeesUnited.Services.Impl
{
	public class SessionService : ISessionService
	{
		private ISessionRepository SessionRepository { get; set; }
		private IAuthenticationService AuthenticationService { get; set; }

		public SessionService(ISessionRepository sessionRepository, IAuthenticationService authenticationService)
		{
			if (sessionRepository == null) throw new ArgumentNullException("sessionRepository");
			if (sessionRepository == null) throw new ArgumentNullException("sessionRepository");

			SessionRepository = sessionRepository;
			AuthenticationService = authenticationService;
		}

		public Session RetrieveSession(long id)
		{
			return SessionRepository.FirstOrDefault(s => s.Id == id);
		}

		public Session CreateSession(string number)
		{
			var device = AuthenticationService.DeviceForPhone(number);
			return SessionRepository.Create(new Session { Device = device });
		}
	}
}
