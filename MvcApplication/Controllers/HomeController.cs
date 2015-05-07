using System.Web.Mvc;
using Company.Web;
using Company.Web.Collections;
using MvcApplication.Models.ViewModels;

namespace MvcApplication.Controllers
{
	public class HomeController : Controller
	{
		#region Fields

		private static readonly INavigationTreeFactory _navigationTreeFactory = new NavigationTreeFactory(new WebContext());

		#endregion

		#region Properties

		protected internal virtual INavigationTreeFactory NavigationTreeFactory
		{
			get { return _navigationTreeFactory; }
		}

		#endregion

		#region Methods

		public virtual ActionResult Index()
		{
			return this.View(new HomeViewModel {Navigation = this.NavigationTreeFactory.Create(4, 3)});
		}

		#endregion
	}
}