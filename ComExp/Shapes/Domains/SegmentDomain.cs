using System.Collections.Generic;

namespace ComExp.Shapes.Domains
{
	public class SegmentDomain:IDomain
	{
		public SegmentDomain(double start, double end)
		{
			Start = start;
			End = end;
		}

		public IEnumerable<double> GetRangeOfArguments()
		{
			return new List<double> {Start, End};
		}

		public IDomain Update(double newPoint)
		{
			return this;
		}

		private double Start { get;  set; }
		private double End { get;  set; }
	}
}