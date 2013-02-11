using RS2013.RefugeesUnited.Model;

namespace RS2013.RefugeesUnited.Services
{
	public interface ISessionService
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Session RetrieveSession(long id);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="number"></param>
		/// <returns></returns>
		Session CreateSession(string number);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="session"></param>
		void TerminateSession(Session session);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="session"></param>
		/// <param name="state"></param>
		/// <param name="data"></param>
		void SetSessionState(Session session, SessionState state, string data = null);
	}
}
