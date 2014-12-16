using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComExp
{
	class Conditions
	{
		public double Epsilon { get; private set; }
		public int MaxNumberOfIteration { get; private set; }
		public IEnumerable<double> InitialPoints { get; private set; }
		public double RootPoint { get; private set; }
	}
}
