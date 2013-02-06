using System.Collections.Generic;

namespace RS2013.RefugeesUnited.Model
{
	public class RefUnitedSearchResponse
	{
		// ReSharper disable InconsistentNaming
		public int count { get; set; }
		public int queryTime { get; set; }
		public IEnumerable<RefUnitedSearchResult> results { get; set; }
		// ReSharper restore InconsistentNaming
	}
}
