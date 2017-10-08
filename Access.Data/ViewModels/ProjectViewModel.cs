namespace Access.Data.ViewModels
{
	public class ProjectViewModel
	{
		/// <summary>
		///     Название проекта
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		///     Клиент
		/// </summary>
		public ClientViewModel Client { get; set; }
	}
}