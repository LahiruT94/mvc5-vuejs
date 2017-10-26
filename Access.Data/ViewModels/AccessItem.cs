namespace Access.Data.ViewModels
{
	public class AccessItem
	{
		public int Id { get; set; }

		/// <summary>
		///     Адрес
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		///     Логин
		/// </summary>
		public string Login { get; set; }

		/// <summary>
		///     Пароль
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		///     Заметка
		/// </summary>
		public string Note { get; set; }

		/// <summary>
		///     Проект
		/// </summary>
		public ProjectItem Project { get; set; }

		/// <summary>
		///     Тип доступа
		/// </summary>
		public AccessTypeItem AccessType { get; set; }
	}
}