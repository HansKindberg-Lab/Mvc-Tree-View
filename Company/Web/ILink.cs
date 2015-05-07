using System;

namespace Company.Web
{
	public interface ILink
	{
		#region Properties

		string Name { get; }
		Uri Url { get; }

		#endregion
	}
}