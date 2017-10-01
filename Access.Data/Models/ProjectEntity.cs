using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Access.Data.Models
{
	/// <summary>
	/// Проекты
	/// </summary>
	public class ProjectEntity : BaseEntity
	{
		/// <summary>
		/// Название проекта
		/// </summary>
		public string Title { get; set; }

		public int ClientId { get; set; }
		/// <summary>
		/// Клиент
		/// </summary>
		[ForeignKey("ClientId")]
		public ClientEntity Client { get; set; }
		/// <summary>
		/// Все доступы проекта
		/// </summary>

		public ICollection<AccessEntity> AccessList { get; set; }
	}
}