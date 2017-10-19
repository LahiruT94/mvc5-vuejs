namespace Access.Data.ViewModels
{
	public class ProjectItem
	{
		public int Id { get; set; }
		/// <summary>
		///     Название проекта
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		///     Клиент
		/// </summary>
		public ClientItem Client { get; set; }

	}
}