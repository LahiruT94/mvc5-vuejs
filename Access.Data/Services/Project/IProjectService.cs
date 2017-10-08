using Access.Data.Models;

namespace Access.Data.Services
{
	public interface IProjectService : IBaseService<ProjectEntity>
	{
		new void Update(ProjectEntity updatedEntity);
	}
}