using System.Collections.Generic;
using Access.Data.Models;
using Access.Data.ViewModels;
using PagedList;

namespace Access.Data.Services
{
	public interface IAccessTypeService : IBaseService<AccessTypeEntity>
	{
		AccessTypeViewModel Get(Filter filter);
		new void Update(AccessTypeEntity model);
		void Delete(int[] id);
	}
}