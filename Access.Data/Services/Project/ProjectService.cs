using System.Data.Entity;
using System.Linq;
using Access.Data.DAL;
using Access.Data.Models;
using Access.Data.ViewModels;
using PagedList;

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

		public IPagedList<ProjectItem> Get(Filter filter)
		{
			var projects = Repository.TableNoTracking.Include(w => w.Client);

			var result = projects.Select(w => new ProjectItem
			{
				Id = w.Id,
				Title = w.Title,
				Client = new ClientItem
				{
					Id = w.Client.Id,
					Title = w.Client.Title,
					Note = w.Client.Note,
					Email = w.Client.Email,
					Phone = w.Client.Phone
				}
			});

			if (filter == null)
				return result.OrderBy(w => w.Id).ToPagedList(1, 10);

			if (!string.IsNullOrWhiteSpace(filter.Search))
				result = result.Where(m => m.Id.ToString().Contains(filter.Search) ||
				                           m.Title.Contains(filter.Search));

			switch (filter.SortColumn)
			{
				case "Title":
				{
					result = filter.SortOrder == "descending" ? result.OrderByDescending(w => w.Title) : result.OrderBy(w => w.Title);
					break;
				}
				default:
				{
					result = result.OrderBy(w => w.Id);
					break;
				}
			}

			return result.ToPagedList(filter.Page, filter.PageSize);
		}

		public void Delete(int[] ids)
		{
			var entities = GetByQuery(w => ids.Contains(w.Id)).ToList();

			if (entities.Count > 0)
				base.Delete(entities);
		}
	}
}