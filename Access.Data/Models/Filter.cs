namespace Access.Data.Models
{
	public class Filter
	{
		public string Search { get; set; }
		public int Page { get; set; }
		public int PageSize { get; set; }
		public string SortColumn { get; set; }
		public string SortOrder { get; set; }
	}
}
