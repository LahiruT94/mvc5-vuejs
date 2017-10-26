using Access.Data.Models;
using Access.Data.ViewModels;
using PagedList;

namespace Access.Data.Services
{
	public interface IAccessService : IBaseService<AccessEntity>
	{
		IPagedList<AccessItem> Get(Filter filter);

		new void Update(AccessEntity updatedEntity);

		void Delete(int[] id);
	}
}