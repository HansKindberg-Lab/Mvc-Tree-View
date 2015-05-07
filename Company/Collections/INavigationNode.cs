using System.Collections.Generic;

namespace Company.Collections
{
	public interface INavigationNode
	{
		#region Properties

		IEnumerable<INavigationNode> Children { get; }

		/// <summary>
		/// If the node is a leaf or not. The node can be a non leaf even if children are empty.
		/// </summary>
		bool IsLeaf { get; }

		bool IsSelected { get; }
		bool IsSelectedAncestor { get; }
		int Level { get; }
		INavigationNode Parent { get; }
		object Value { get; }

		#endregion
	}

	public interface INavigationNode<out T> : INavigationNode
	{
		#region Properties

		new IEnumerable<INavigationNode<T>> Children { get; }
		new INavigationNode<T> Parent { get; }
		new T Value { get; }

		#endregion
	}
}