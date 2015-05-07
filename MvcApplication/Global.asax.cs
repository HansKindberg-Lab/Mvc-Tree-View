using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml;
using System.Xml.Linq;
using Company.Web;

namespace MvcApplication
{
	public class Global : HttpApplication
	{
		#region Methods

		protected void Application_AuthenticateRequest(object sender, EventArgs e) {}
		protected void Application_BeginRequest(object sender, EventArgs e) {}
		protected void Application_End(object sender, EventArgs e) {}

		protected void Application_EndRequest(object sender, EventArgs e)
		{
			var captureStream = (CaptureStream) this.Response.Filter;

			captureStream.MemoryStream.Position = 0;

			using(var streamReader = new StreamReader(captureStream.MemoryStream))
			{
				var response = streamReader.ReadToEnd();

				try
				{
					// ReSharper disable ReturnValueOfPureMethodIsNotUsed
					XDocument.Parse(response);
					// ReSharper restore ReturnValueOfPureMethodIsNotUsed
				}
				catch(XmlException xmlException)
				{
					throw new InvalidOperationException("The html is invalid. Mismatching start and end tags.", xmlException);
				}
			}

			captureStream.MemoryStream.Close();
		}

		protected void Application_Error(object sender, EventArgs e) {}

		protected void Application_PostReleaseRequestState(object sender, EventArgs e)
		{
			this.Response.Filter = new CaptureStream(this.Response.Filter);
		}

		protected void Application_Start(object sender, EventArgs e)
		{
			AreaRegistration.RegisterAllAreas();

			RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			RouteTable.Routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
				);
		}

		protected void Session_End(object sender, EventArgs e) {}
		protected void Session_Start(object sender, EventArgs e) {}

		#endregion
	}
}