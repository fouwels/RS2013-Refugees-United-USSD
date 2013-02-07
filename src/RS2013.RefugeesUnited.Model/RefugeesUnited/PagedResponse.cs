using System.Collections.Generic;

namespace RS2013.RefugeesUnited.Model.RefugeesUnited
{
	public class PagedResponse<T>
	{
		// ReSharper disable InconsistentNaming
		public int count { get; set; }
		public int queryTime { get; set; }
		public IEnumerable<T> results { get; set; }
		// ReSharper restore InconsistentNaming
	}
}
