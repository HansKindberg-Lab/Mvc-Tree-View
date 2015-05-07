using System;
using System.Globalization;
using System.Web;
using Company.Collections;

namespace Company.Web.Collections
{
	public class NavigationTreeFactory : INavigationTreeFactory
	{
		#region Fields

		private const string _pathParameterName = "Path";
		private readonly IWebContext _webContext;

		#endregion

		#region Constructors

		public NavigationTreeFactory(IWebContext webContext)
		{
			if(webContext == null)
				throw new ArgumentNullException("webContext");

			this._webContext = webContext;
		}

		#endregion

		#region Properties

		protected internal virtual string PathParameterName
		{
			get { return _pathParameterName; }
		}

		protected internal virtual IWebContext WebContext
		{
			get { return this._webContext; }
		}

		#endregion

		#region Methods

		protected internal virtual NavigationNode<Link> Create(string path)
		{
			if(path == null)
				throw new ArgumentNullException("path");

			if(string.IsNullOrWhiteSpace(path))
				throw new ArgumentException("The path can not be empty.", "path");

			var contextPath = this.WebContext.QueryString[this.PathParameterName];

			return new NavigationNode<Link>
			{
				IsLeaf = true,
				IsSelected = this.IsSelected(path, contextPath),
				IsSelectedAncestor = this.IsSelectedAncestor(path, contextPath),
				Level = path.Split(".".ToCharArray()).Length - 1,
				Value = new Link
				{
					Path = path,
					Url = this.CreateUrl(path)
				}
			};
		}

		public virtual INavigationNode<ILink> Create(int numberOfLevels, int numberOfSiblings)
		{
			if(numberOfLevels < 1)
				return null;

			var root = this.Create(this.CreatePath(null, 0));

			this.PopulateNavigationNode(root, numberOfLevels, numberOfSiblings);

			return root;
		}

		protected internal virtual string CreatePath(string parentPath, int siblingIndex)
		{
			if(siblingIndex == int.MaxValue)
				throw new OverflowException(string.Format(CultureInfo.InvariantCulture, "The sibling-index must be less than {0}.", int.MaxValue));

			var path = (siblingIndex + 1).ToString(CultureInfo.InvariantCulture);

			if(string.IsNullOrWhiteSpace(parentPath))
				return path;

			return parentPath + "." + path;
		}

		protected internal virtual Uri CreateUrl(string path)
		{
			var uriBuilder = new UriBuilder(this.WebContext.Url);

			var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);

			if(string.IsNullOrWhiteSpace(path))
				queryString.Remove(this.PathParameterName);
			else
				queryString.Set(this.PathParameterName, path);

			uriBuilder.Query = queryString.ToString();

			return uriBuilder.Uri;
		}

		protected internal virtual bool IsSelected(string path, string contextPath)
		{
			return string.Equals(path, contextPath, StringComparison.OrdinalIgnoreCase);
		}

		protected internal virtual bool IsSelectedAncestor(string path, string contextPath)
		{
			if(path == null)
				throw new ArgumentNullException("path");

			if(string.IsNullOrWhiteSpace(contextPath))
				return false;

			return path.Length < contextPath.Length && contextPath.StartsWith(path, StringComparison.OrdinalIgnoreCase);
		}

		protected internal virtual void PopulateNavigationNode(NavigationNode<Link> navigationNode, int numberOfLevels, int numberOfChildren)
		{
			if(navigationNode == null)
				throw new ArgumentNullException("navigationNode");

			if(numberOfLevels < 1)
				return;

			if(numberOfChildren > 0 && navigationNode.Level < numberOfLevels - 1)
				navigationNode.IsLeaf = false;

			if(navigationNode.Level >= numberOfLevels - 1)
				return;

			if(!navigationNode.IsSelected && !navigationNode.IsSelectedAncestor)
				return;

			for(var i = 0; i < numberOfChildren; i++)
			{
				var child = this.Create(this.CreatePath(navigationNode.Value.Path, i));

				child.Parent = navigationNode;

				navigationNode.Children.Add(child);

				this.PopulateNavigationNode(child, numberOfLevels, numberOfChildren);
			}
		}

		#endregion
	}
}