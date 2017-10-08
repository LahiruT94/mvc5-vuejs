using System.Collections.Generic;

namespace Access.Data.Models
{
	/// <summary>
	///     Клиенты
	/// </summary>
	public class ClientEntity : BaseEntity
	{
		/// <summary>
		///     Имя клиента
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		///     Email клиента
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		///     Номер телефона клиента
		/// </summary>
		public string Phone { get; set; }

		/// <summary>
		///     Заметка
		/// </summary>
		public string Note { get; set; }

		/// <summary>
		///     Все проекты клиента
		/// </summary>
		public ICollection<ProjectEntity> ProjectList { get; set; }
	}
}