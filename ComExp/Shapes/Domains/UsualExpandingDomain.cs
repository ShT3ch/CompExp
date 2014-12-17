using System.Collections.Generic;

namespace ComExp.Shapes.Domains
{
	public class UsualExpandingDomain : IDomain
	{
		public IEnumerable<double> GetRangeOfArguments()
		{
			var current = Start;
			while (Finish - current >= Delta)
			{
				yield return current;
				current += Delta;
			}
			yield return Finish;
		}

		public IDomain Update(double newPoint)
		{
			if (newPoint > Finish)
				Finish = newPoint;
			if (newPoint < Start)
				Start = newPoint;

			return this;
		}

		private const double Delta = 0.1;

		private bool Initialized;

		private double Start = double.PositiveInfinity;
		private double Finish = double.NegativeInfinity;
	}
}
