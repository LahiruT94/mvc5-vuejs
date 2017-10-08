using System.Collections.Generic;
using PagedList;

namespace Access.Data.ViewModels
{
	public class AccessTypeViewModel
	{
		public IPagedList<AccessTypeItem> Items { get; set; }
		public int Total { get; set; }
	}
}