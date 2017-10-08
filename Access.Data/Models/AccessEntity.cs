namespace Access.Data.Models
{
	/// <summary>
	///     Доступы
	/// </summary>
	public class AccessEntity : BaseEntity
	{
		public int ProjectId { get; set; }
		public int AccessTypeId { get; set; }

		/// <summary>
		///     Проект
		/// </summary>
		public ProjectEntity Project { get; set; }

		/// <summary>
		///     Тип доступа
		/// </summary>
		public AccessTypeEntity AccessType { get; set; }

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
	}
}