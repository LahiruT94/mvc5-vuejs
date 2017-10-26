using System.Linq;
using Access.Data.DAL;
using Access.Data.Models;
using Access.Data.ViewModels;
using PagedList;

namespace Access.Data.Services
{
	public class AccessService : BaseService<AccessEntity>, IAccessService
	{
		public AccessService(IRepository<AccessEntity> repository)
			: base(repository)
		{
		}

		public IPagedList<AccessItem> Get(Filter filter)
		{
			var result = Repository.TableNoTracking.Select(a => new AccessItem
			{
				Id = a.Id,
				Note = a.Note,
				Password = a.Password,
				Address = a.Address,
				Login = a.Login,
				AccessType = new AccessTypeItem
				{
					Id = a.AccessType.Id,
					Title = a.AccessType.Title
				},
				Project = new ProjectItem
				{
					Title = a.Project.Title,
					Id = a.Project.Id
				}
			});

			if (filter == null)
				return result.OrderBy(w => w.Id).ToPagedList(1, 10);

			if (!string.IsNullOrWhiteSpace(filter.Search))
				result = result.Where(m => m.Id.ToString().Contains(filter.Search) ||
				                           m.Address.Contains(filter.Search) ||
				                           m.Login.Contains(filter.Search) ||
				                           m.Password.Contains(filter.Search) ||
				                           m.Note.Contains(filter.Search));

			switch (filter.SortColumn)
			{
				case "Address":
				{
					result = filter.SortOrder == "descending" ? result.OrderByDescending(w => w.Address) : result.OrderBy(w => w.Address);
					break;
				}
				case "Login":
				{
					result = filter.SortOrder == "descending" ? result.OrderByDescending(w => w.Login) : result.OrderBy(w => w.Login);
					break;
				}
				case "Password":
				{
					result = filter.SortOrder == "descending" ? result.OrderByDescending(w => w.Password) : result.OrderBy(w => w.Password);
					break;
				}
				case "Note":
				{
					result = filter.SortOrder == "descending" ? result.OrderByDescending(w => w.Note) : result.OrderBy(w => w.Note);
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

		public override void Update(AccessEntity updatedEntity)
		{
			var access = GetById(updatedEntity.Id);
			access.Address = updatedEntity.Address;
			access.AccessTypeId = updatedEntity.AccessTypeId;
			access.Login = updatedEntity.Login;
			access.Password = updatedEntity.Password;
			access.Note = updatedEntity.Note;
			access.ProjectId = updatedEntity.ProjectId;

			base.Update(access);
		}

		public void Delete(int[] ids)
		{
			var entities = GetByQuery(w => ids.Contains(w.Id)).ToList();

			if (entities.Count > 0)
				base.Delete(entities);
		}
	}
}