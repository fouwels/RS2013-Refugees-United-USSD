using System;

namespace RS2013.RefugeesUnited.Model
{
	public partial class Session
	{
		public Session()
		{
			ActivityTimestamp = DateTime.Now;
			StartTimestamp = DateTime.Now;
			State = SessionState.AuthenticationOptions;
		}
	}
}
