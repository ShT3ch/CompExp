using System.Collections.Generic;

namespace ComExp.Shapes.Domains
{
	public interface IDomain
	{
		IEnumerable<double> GetRangeOfArguments();
		IDomain Update(double newPoint);
	}
}
