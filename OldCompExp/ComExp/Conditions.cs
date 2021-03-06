﻿using System;
using System.Collections.Generic;

namespace ComExp
{
	public interface Conditions
	{

		double Epsilon { get; }
		int MaxNumberOfIteration { get; }
		IEnumerable<double> InitialPoints { get; }
		double RootPoint { get; }
	}

	public class Conditions4 : Conditions
	{
		public Conditions4()
		{
			Epsilon = 0.5 * Math.Pow(10, -5);
			MaxNumberOfIteration = 120;
			InitialPoints = new List<double> { 1, 0, 4, };
			RootPoint = 0.539785;
		}

		public double Epsilon { get; private set; }
		public int MaxNumberOfIteration { get; private set; }
		public IEnumerable<double> InitialPoints { get; private set; }
		public double RootPoint { get; private set; }
	}

	public class Conditions1 : Conditions
	{
		public Conditions1()
		{
			Epsilon = 0.5 * Math.Pow(10, -5);
			MaxNumberOfIteration = 120;
			InitialPoints = new List<double> { 5,4.6, 6, };
			RootPoint = 4.8495941;
		}

		public double Epsilon { get; private set; }
		public int MaxNumberOfIteration { get; private set; }
		public IEnumerable<double> InitialPoints { get; private set; }
		public double RootPoint { get; private set; }


	}

	public class Conditions3 : Conditions
	{
		public Conditions3()
		{
			Epsilon = 0.5 * Math.Pow(10, -5);
			MaxNumberOfIteration = 13;
			InitialPoints = new List<double> { 2, 1.5, 1 };
			RootPoint = 1.37471;
		}

		public double Epsilon { get; private set; }
		public int MaxNumberOfIteration { get; private set; }
		public IEnumerable<double> InitialPoints { get; private set; }
		public double RootPoint { get; private set; }


	}
}
