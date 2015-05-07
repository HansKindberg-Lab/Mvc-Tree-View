using Company.Collections;
using Company.Web;

namespace MvcApplication.Models.ViewModels
{
	public class HomeViewModel
	{
		#region Properties

		public virtual INavigationNode<ILink> Navigation { get; set; }

		#endregion
	}
}