using System.Collections.Generic;
using Access.Data.Models;
using Access.Data.ViewModels;

namespace Access.Data.Services
{
	public interface IClientService : IBaseService<ClientEntity>
	{
		IEnumerable<AccessListViewModel> GetAll(int page, int pageSize, string filter, string orderKey);

		new void Update(ClientEntity updatedEntity);
	}
}