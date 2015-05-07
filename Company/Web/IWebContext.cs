using System;
using System.Collections.Specialized;
using System.Web;

namespace Company.Web
{
	public interface IWebContext
	{
		#region Properties

		HttpContextBase HttpContext { get; }
		HttpRequestBase HttpRequest { get; }
		NameValueCollection QueryString { get; }
		Uri Url { get; }

		#endregion
	}
}