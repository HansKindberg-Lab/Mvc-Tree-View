using System;
using Company.Web;
using Company.Web.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests.Web.Collections
{
	[TestClass]
	public class NavigationTreeFactoryTest
	{
		#region Fields

		//private static readonly Uri _defaultUrl = new Uri("http://localhost");
		private static readonly Random _random = new Random(DateTime.Now.Millisecond);

		#endregion

		#region Methods

		[TestMethod]
		public void Create_IfTheNumberOfLevelsParameterIsLessThanOne_ShouldReturnNull()
		{
			var numberOfLevels = GetRandomNumberLessThanOne();
			var numberOfSiblings = GetRandomNumberMoreThanOne();

			Assert.IsTrue(numberOfLevels < 1);
			Assert.IsTrue(numberOfSiblings > 1);

			Assert.IsNull(new NavigationTreeFactory(Mock.Of<IWebContext>()).Create(numberOfLevels, numberOfSiblings));
		}

		//[TestMethod]
		//[ExpectedException(typeof(ArgumentNullException))]
		//public void Create_IfTheUrlParameterIsNull_ShouldShouldThrowAnArgumentNullException()
		//{
		//	var numberOfLevels = GetRandomNumberLessThanOne();
		//	var numberOfSiblings = GetRandomNumberMoreThanOne();

		//	Assert.IsTrue(numberOfLevels < 1);
		//	Assert.IsTrue(numberOfSiblings > 1);

		//	Assert.IsNull(new HomeViewModel(DefaultUrl).CreateNavigation(numberOfLevels, numberOfSiblings));
		//}
		private static int GetRandomNumberLessThanOne()
		{
			return _random.Next(int.MinValue, 0);
		}

		private static int GetRandomNumberMoreThanOne()
		{
			return _random.Next(1, int.MaxValue);
		}

		#endregion
	}
}