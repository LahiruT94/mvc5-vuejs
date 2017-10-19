using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Access.Data.ViewModels
{
	public class AccessViewModel
	{
		public int AccessTypeId { get; set; }

		/// <summary>
		///     Проект
		/// </summary>
		public List<ProjectItem> Project { get; set; }

		/// <summary>
		///     Тип доступа
		/// </summary>
		public List<AccessTypeItem> AccessType { get; set; }

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
