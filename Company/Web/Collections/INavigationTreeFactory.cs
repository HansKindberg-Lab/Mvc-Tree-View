using Company.Collections;

namespace Company.Web.Collections
{
	public interface INavigationTreeFactory
	{
		#region Methods

		INavigationNode<ILink> Create(int numberOfLevels, int numberOfSiblings);

		#endregion
	}
}