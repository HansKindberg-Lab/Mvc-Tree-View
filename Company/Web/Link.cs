using System;

namespace Company.Web
{
	public class Link : ILink
	{
		#region Properties

		public virtual string Name
		{
			get
			{
				if(string.IsNullOrWhiteSpace(this.Path))
					return "Node";

				return "Node " + this.Path;
			}
		}

		public virtual string Path { get; set; }
		public virtual Uri Url { get; set; }

		#endregion
	}
}