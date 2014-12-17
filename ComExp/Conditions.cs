using System;
using System.Collections.Generic;

namespace ComExp
{
	class Conditions
	{
		public Conditions()
		{
			Epsilon = 0.5*Math.Pow(10, -5);
			MaxNumberOfIteration = 100;
			InitialPoints = new List<double> {1, 2, 3};
			RootPoint = 1;
		}

		public double Epsilon { get; private set; }
		public int MaxNumberOfIteration { get; private set; }
		public IEnumerable<double> InitialPoints { get; private set; }
		public double RootPoint { get; private set; }


	}
}
