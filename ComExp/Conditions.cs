using System;
using System.Collections.Generic;

namespace ComExp
{
	public class Conditions
	{
		public Conditions()
		{
			Epsilon = 0.5*Math.Pow(10, -5);
			MaxNumberOfIteration = 13;
			InitialPoints = new List<double> {0, 2};
			RootPoint = 0.539785;
		}

		public double Epsilon { get; private set; }
		public int MaxNumberOfIteration { get; private set; }
		public IEnumerable<double> InitialPoints { get; private set; }
		public double RootPoint { get; private set; }


	}
}
