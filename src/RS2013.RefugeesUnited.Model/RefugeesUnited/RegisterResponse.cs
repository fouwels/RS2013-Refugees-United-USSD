namespace RS2013.RefugeesUnited.Model.RefugeesUnited
{
	public class RegisterResponse
	{
		// ReSharper disable InconsistentNaming
		public class Profile
		{
			public int id { get; set; }
		}

		public class RootObject
		{
			public Profile profile { get; set; }
		}
		// ReSharper restore InconsistentNaming
	}
}
