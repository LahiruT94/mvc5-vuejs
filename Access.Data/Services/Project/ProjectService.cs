using Access.Data.DAL;
using Access.Data.Models;

namespace Access.Data.Services
{
	public class ProjectService : BaseService<ProjectEntity>, IProjectService
	{
		public ProjectService(IRepository<ProjectEntity> repository)
			: base(repository)
		{
		}

		public override void Update(ProjectEntity updatedEntity)
		{
			var project = GetById(updatedEntity.Id);
			project.ClientId = updatedEntity.ClientId;
			project.Title = updatedEntity.Title;

			base.Update(project);
		}
	}
}