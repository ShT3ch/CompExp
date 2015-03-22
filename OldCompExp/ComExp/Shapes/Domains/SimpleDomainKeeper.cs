using System.Collections.Generic;

namespace ComExp.Shapes.Domains
{
	public class SimpleDomainKeeper:IDomain
	{
		private IEnumerable<double> StoredEnum { get; set; }

		public SimpleDomainKeeper(IEnumerable<double> storedEnum)
		{
			StoredEnum = storedEnum;
		}

		public IEnumerable<double> GetRangeOfArguments()
		{
			return StoredEnum;
		}

		public IDomain Update(double newPoint)
		{
			return this;
		}
	}
}