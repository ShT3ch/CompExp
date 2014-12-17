using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComExp;
using ComExp.Methods;
using ComExp.Reporters;
using ComExp.Shapes;
using ComExp.Shapes.Domains;
using ComExp.Shapes.Functions;
using ComExp.Visualization;

namespace FixedChord4
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			var host = new PlotHost();
			host.Initialize();
			var plot = new ShapesPlot();
			host.AccomodateControl(plot);

			var func = new Problem3();
			var shape = new ShapeKeeper<Problem3>(func, new UsualExpandingDomain(), "analized function");
			var conditions = new Conditions3();
			var cycle = new Iterator<NewtonMethod, Problem3>(shape, conditions);
			var method = new NewtonMethod();//(conditions.InitialPoints.Min(), conditions.InitialPoints.Max(), func, -1);

			var reporter = new SimpleReporter();

			Task.Run(() =>
						{
							host.InitializedEvent.WaitOne();
							cycle.DoItRight(method, shape, plot, conditions, reporter);
							Console.WriteLine(reporter.GenerateReport());
						}
				);
			host.InitializedEvent.Set();
			Application.Run(PlotHost.Main);
		}
	}
}
