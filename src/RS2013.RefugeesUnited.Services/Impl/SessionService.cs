using System;
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
			var session = SessionRepository.Get(id);
			
			if (session.State == SessionState.Terminated
				|| session.Device.BlacklistReason != null
				|| (session.User != null && session.User.BlacklistReason != null))
				throw new UnauthorizedAccessException();

			return session;
		}

		public Session CreateSession(string number)
		{
			var device = AuthenticationService.DeviceForPhone(number);

			if (device.BlacklistReason != null)
				throw new UnauthorizedAccessException();

			return SessionRepository.Create(new Session { Device = device });
		}

		public void TerminateSession(Session session)
		{
			SessionRepository.Change(session, (ref Session s) => s.State = SessionState.Terminated);
		}

		public void SetSessionState(Session session, SessionState state, string data = null)
		{
			SessionRepository.Change(session, delegate(ref Session s)
			{
				s.State = state;

				if (data != null)
					s.StateJson = data;
			});
		}
	}
}
