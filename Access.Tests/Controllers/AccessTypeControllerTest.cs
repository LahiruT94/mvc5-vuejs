using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Access.API;
using Access.Data.Models;
using Access.Data.Services;

namespace Access.Tests.Controllers
{
	[TestClass]
	public class AccessTypeControllerTest
	{
		private IAccessTypeService _accessTypeService;
		[TestMethod]
		public void Index(IAccessTypeService accessTypeService)
		{
			_accessTypeService = accessTypeService;
			// Arrange
			var controller = new AccessTypeController(_accessTypeService);

			var filter = new Filter
			{
				Search = "",
				SortOrder = "",
				PageSize = 10,
				SortColumn = "",
				Page = 1
			};
			// Act
			var result = controller.Get(filter);

			// Assert
			Assert.IsNotNull(result);
			
		}
	}
}