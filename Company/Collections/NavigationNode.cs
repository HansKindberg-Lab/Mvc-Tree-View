using System.Collections.Generic;

namespace Company.Collections
{
	public class NavigationNode<T> : INavigationNode<T>
	{
		#region Fields

		private readonly IList<INavigationNode<T>> _children = new List<INavigationNode<T>>();

		#endregion

		#region Properties

		IEnumerable<INavigationNode> INavigationNode.Children
		{
			get { return this.Children; }
		}

		IEnumerable<INavigationNode<T>> INavigationNode<T>.Children
		{
			get { return this.Children; }
		}

		public virtual IList<INavigationNode<T>> Children
		{
			get { return this._children; }
		}

		public virtual bool IsLeaf { get; set; }
		public virtual bool IsSelected { get; set; }
		public virtual bool IsSelectedAncestor { get; set; }
		public virtual int Level { get; set; }

		INavigationNode INavigationNode.Parent
		{
			get { return this.Parent; }
		}

		public virtual INavigationNode<T> Parent { get; set; }
		public virtual T Value { get; set; }

		object INavigationNode.Value
		{
			get { return this.Value; }
		}

		#endregion
	}
}