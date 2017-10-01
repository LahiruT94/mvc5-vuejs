using System.Web.Mvc;
using Access.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Access.Tests.Controllers
{
	[TestClass]
	public class HomeControllerTest
	{
		[TestMethod]
		public void Index()
		{
			// Arrange
			var controller = new HomeController();

			// Act
			var result = controller.Index() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
		}
	}
}