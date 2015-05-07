using System;
using System.Collections.Specialized;
using System.Web;

namespace Company.Web
{
	public class WebContext : IWebContext
	{
		#region Properties

		public virtual HttpContextBase HttpContext
		{
			get { return new HttpContextWrapper(System.Web.HttpContext.Current); }
		}

		public virtual HttpRequestBase HttpRequest
		{
			get { return this.HttpContext.Request; }
		}

		public virtual NameValueCollection QueryString
		{
			get { return this.HttpRequest.QueryString; }
		}

		public virtual Uri Url
		{
			get { return this.HttpRequest.Url; }
		}

		#endregion
	}
}