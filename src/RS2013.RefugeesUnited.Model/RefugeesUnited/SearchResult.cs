namespace RS2013.RefugeesUnited.Model.RefugeesUnited
{
	public class SearchResult
	{
		// ReSharper disable InconsistentNaming
		public string profileid { get; set; }
		public string givenName { get; set; }
		public string surName { get; set; }
		public int? age { get; set; }
		public int? countryOfBirthId { get; set; }
		public int? genderId { get; set; }
		public bool isMissingPerson { get; set; }
		// ReSharper restore InconsistentNaming
	}
}
