using System;
using ComExp.Shapes.Domains;
using ComExp.Shapes.Functions;
using ComExp.Shapes.Functions.FuncInterfaces;

namespace ComExp.Shapes
{
	public class LineShape : IShape
	{
		public LineShape(IDomain segment, Point2D start, Point2D end, string name)
		{
			ParametersDomain = segment;

			Start = start;
			End = end;
			Name = name;

			LineEquation = x => (End.Y - Start.Y) * (x - start.X) / (End.X - Start.X) + Start.Y;

			Generator = new SimpleFunctionKeeper(LineEquation);
		}

		private Func<double, double> LineEquation;

		private Point2D Start { get; set; }
		private Point2D End { get; set; }


		public IFunction Generator { get; private set; }
		public IDomain ParametersDomain { get; private set; }

		public string Name { get; private set; }
	}

	public class Point2D
	{
		public double X;
		public double Y;
	}
}