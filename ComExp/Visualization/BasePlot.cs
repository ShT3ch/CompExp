using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ILNumerics;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;

namespace ComExp.Visualization
{
	public class BasePlot : ILPanel
	{
		public BasePlot()
		{
			SetUpHostFormAndEnvirement();
			CreatePlot();
		}

		public PointsGroup AddPoints(List<double[]> points)
		{
			var graph = new PointsGroup();
			Space.Add(graph.Graph);

			points.ForEach(graph.AddPoint);
			return graph;
		}

		public void ExportImage(string path = "")
		{
			using (var fs = new FileStream(path, FileMode.Create))
			{
				new ILSVGDriver(fs, scene: this.Scene).Render();
			}
		}

		protected void DoNessesaryMagicForStartUp()
		{
			Load += (sender, args) =>
					{
						using (ILScope.Enter())
						{
							Clock.Running = true;
						}
					};
			Configure();
		}

		private void SetUpHostFormAndEnvirement()
		{
			AutoSizeMode = AutoSizeMode.GrowAndShrink;
			Driver = System.Diagnostics.Debugger.IsAttached ? RendererTypes.GDI : RendererTypes.OpenGL;
		}

		private void CreatePlot()
		{
			Scene = new ILScene();

			Scene.Add(Space = new ILPlotCube(twoDMode: true));
		}

		protected void IUpdated()
		{
			Updated.Invoke();
		}

		public event Action Updated = () => { };
		public ILPlotCube Space { get; set; }
	}
}