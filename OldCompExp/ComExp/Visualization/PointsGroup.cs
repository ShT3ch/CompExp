using System.Drawing;
using ILNumerics;
using ILNumerics.Drawing;

namespace ComExp.Visualization
{
	public class PointsGroup
	{
		public readonly ILPoints Graph = CreateGraph();
		private const int ArraySize = 3000;
		private readonly float[,] Points = new float[ArraySize, 3];
		private int Counter;

		public PointsGroup(Color color)
		{
			Graph.Color = color;
		}

		public PointsGroup()
		{
		}

		public void AddPoint(double[] point)
		{
			AddData(point);

			ApplyToGraph();
		}

		public void ClearPoints()
		{
			for (int i = 0; i < Counter; i++)
			{
				Points[Counter, 0] = 0;
				Points[Counter, 1] = 0;
				Points[Counter, 2] = 0;
			}

			ApplyToGraph();

			Counter = 0;
		}

		private void ApplyToGraph()
		{
			var data = (ILArray<float>) Points;
			Graph.Positions.Update(data);
			Graph.Configure();
		}

		private void AddData(double[] point)
		{
			Points[Counter, 0] = (float) point[0];
			Points[Counter, 1] = (float) point[1];
			Points[Counter, 2] = 0;

			Counter += 1;
		}

		private static ILPoints CreateGraph()
		{
			return new ILPoints();
		}
	}
}