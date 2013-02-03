using RS2013.RefugeesUnited.Model;

namespace RS2013.RefugeesUnited.Services
{
	public interface ISessionService
	{
		Session RetrieveSession(string ussdSessionId);
	}
}
