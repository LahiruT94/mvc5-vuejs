using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Access.Data.Models;
using Access.Data.Services;

namespace Access.Data.Services
{
	public interface IAccessTypeService : IBaseService<AccessTypeEntity>
	{
		new void Update(AccessTypeEntity model);
		void Delete(List<int> id);

	}
}
