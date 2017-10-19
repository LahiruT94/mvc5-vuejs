using System.Collections.Generic;
using Access.Data.Models;
using Access.Data.ViewModels;
using PagedList;

namespace Access.Data.Services
{
	public interface IClientService : IBaseService<ClientEntity>
	{
		IEnumerable<AccessListViewModel> GetAll(int page, int pageSize, string filter, string orderKey);

		IPagedList<ClientItem> Get(Filter filter);

		new void Update(ClientEntity updatedEntity);

		void Delete(int[] id);
	}
}