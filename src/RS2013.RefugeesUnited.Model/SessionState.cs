namespace RS2013.RefugeesUnited.Model
{
	public enum SessionState : byte
	{
		Terminated = 0,
		AuthenticationOptions = 1,
		ConnectAccount = 2,
		Register = 3,
		Login = 4,
		LoginCode = 5,
		MainMenu = 6
	}
}
