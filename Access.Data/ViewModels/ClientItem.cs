namespace Access.Data.ViewModels
{
	public class ClientItem
	{
		public int Id { get; set; }

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
	}
}