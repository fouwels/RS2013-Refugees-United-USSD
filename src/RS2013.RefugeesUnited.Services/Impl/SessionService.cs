using System;
using System.Linq;
using RS2013.RefugeesUnited.Data;
using RS2013.RefugeesUnited.Model;

namespace RS2013.RefugeesUnited.Services.Impl
{
	public class SessionService : ISessionService
	{
		private ISessionRepository SessionRepository { get; set; }

		public SessionService(ISessionRepository sessionRepository)
		{
			if (sessionRepository == null) throw new ArgumentNullException("sessionRepository");

			SessionRepository = sessionRepository;
		}

		public Session RetrieveSession(string ussdSessionId)
		{
			return SessionRepository.FirstOrDefault(s => s.UssdId == ussdSessionId);
		}
	}
}
