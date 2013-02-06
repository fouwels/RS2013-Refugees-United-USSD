namespace RS2013.RefugeesUnited.Model.RefugeesUnited
{
	public class LoginResponse
	{
		// ReSharper disable InconsistentNaming
		public bool authenticated { get; set; }
		public bool verificationRequired { get; set; }
		public bool forcePasswordReset { get; set; }
		// ReSharper restore InconsistentNaming
	}
}
