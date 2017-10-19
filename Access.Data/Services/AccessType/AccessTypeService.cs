using System.Linq;
using Access.Data.DAL;
using Access.Data.Models;
using Access.Data.ViewModels;
using PagedList;

namespace Access.Data.Services
{
	public class AccessTypeService : BaseService<AccessTypeEntity>, IAccessTypeService
	{
		public AccessTypeService(IRepository<AccessTypeEntity> repository)
			: base(repository)
		{
		}

		public IPagedList<AccessTypeItem> Get(Filter filter)
		{
			var result = Repository.TableNoTracking.Select(a => new AccessTypeItem
			{
				Id = a.Id,
				Title = a.Title
			});

			if (filter == null)
				return result.OrderBy(w => w.Id).ToPagedList(1, 10);

			if (!string.IsNullOrWhiteSpace(filter.Search))
				result = result.Where(m => m.Title.Contains(filter.Search) ||
				                           m.Id.ToString().Contains(filter.Search));

			result = filter.SortOrder == "descending" ? result.OrderByDescending(w => w.Title) : result.OrderBy(w => w.Title);

			return result.ToPagedList(filter.Page, filter.PageSize);
		}

		public override void Update(AccessTypeEntity updatedEntity)
		{
			var accessType = GetById(updatedEntity.Id);
			if (!string.IsNullOrWhiteSpace(updatedEntity.Title))
				accessType.Title = updatedEntity.Title;

			base.Update(accessType);
		}

		public void Delete(int[] ids)
		{
			var entities = GetByQuery(w => ids.Contains(w.Id)).ToList();

			if (entities.Count > 0)
				base.Delete(entities);
		}
	}
}