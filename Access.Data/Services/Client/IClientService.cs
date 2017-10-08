using System.Collections.Generic;
using Access.Data.Models;
using Access.Data.ViewModels;

namespace Access.Data.Services
{
	public interface IClientService : IBaseService<ClientEntity>
	{
		new IEnumerable<AccessListViewModel> GetAll();

		new void Update(ClientEntity updatedEntity);
	}
}