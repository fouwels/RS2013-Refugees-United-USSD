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
	}
}
