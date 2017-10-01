using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Access.Data.Models
{
	/// <summary>
	/// Тип доступа
	/// </summary>
	public class AccessTypeEntity : BaseEntity
	{
		/// <summary>
		/// Название доступа
		/// </summary>
		public string Title { get; set; }

		public ICollection<AccessEntity> AccessList { get; set; }
	}
}