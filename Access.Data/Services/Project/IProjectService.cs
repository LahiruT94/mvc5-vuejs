using Access.Data.Models;
using Access.Data.ViewModels;
using PagedList;

namespace Access.Data.Services
{
	public interface IProjectService : IBaseService<ProjectEntity>
	{
		new void Update(ProjectEntity updatedEntity);

		IPagedList<ProjectItem> Get(Filter filter);

		void Delete(int[] id);
	}
}