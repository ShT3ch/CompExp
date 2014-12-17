﻿using System;
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

			var cycle = new Iterator<SecantMethod, Problem4>();

			var method = new SecantMethod();
			var func = new ShapeKeeper<Problem4>(new Problem4(), new UsualExpandingDomain(), "analized function");
			var reporter = new SimpleReporter();

			Task.Run(() =>
						{
							host.InitializedEvent.WaitOne();
							cycle.DoItRight(method, func, plot, new Conditions(), reporter);
							Console.WriteLine(reporter.GenerateReport());
						}
				);
			host.InitializedEvent.Set();
			Application.Run(PlotHost.Main);
		}
	}
}
