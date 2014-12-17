using System;
using System.Collections.Generic;

namespace ComExp
{
	public interface Conditions
	{

		double Epsilon { get; }
		int MaxNumberOfIteration { get; }
		IEnumerable<double> InitialPoints { get; }
		double RootPoint { get;}
	}

	public class Conditions4:Conditions
	{
		public Conditions4()
		{
			Epsilon = 0.5 * Math.Pow(10, -5);
			MaxNumberOfIteration = 13;
			InitialPoints = new List<double> { 0, 3, };
			RootPoint = 0.539785;
		}

		public double Epsilon { get; private set; }
		public int MaxNumberOfIteration { get; private set; }
		public IEnumerable<double> InitialPoints { get; private set; }
		public double RootPoint { get; private set; }


	}

	public class Conditions3:Conditions
	{
		public Conditions3()
		{
			Epsilon = 0.5 * Math.Pow(10, -5);
			MaxNumberOfIteration = 13;
			InitialPoints = new List<double> { 1,1.5,2};
			RootPoint = 1.37471;
		}

		public double Epsilon { get; private set; }
		public int MaxNumberOfIteration { get; private set; }
		public IEnumerable<double> InitialPoints { get; private set; }
		public double RootPoint { get; private set; }


	}
}
