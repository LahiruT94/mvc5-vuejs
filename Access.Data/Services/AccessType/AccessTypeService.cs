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

		public AccessTypeViewModel Get(Filter filter)
		{
			var items = Repository.Table.Select(a => new AccessTypeItem
			{
				Id = a.Id,
				Title = a.Title
			});

			var result = new AccessTypeViewModel();

			if (filter == null)
			{
				result.Items = items.OrderBy(w => w.Title).ToPagedList(1, 5);
				return result;
			}
				

			if (!string.IsNullOrWhiteSpace(filter.Search))
				items = items.Where(m => m.Title.Contains(filter.Search));

			items = filter.SortOrder == "descending" ? items.OrderByDescending(w => w.Title) : items.OrderBy(w => w.Title);

			var totalItems = items;

			result.Items = items.ToPagedList(filter.Page, filter.PageSize);
			result.Total = totalItems.Count();

			return result;
		}

		public override void Update(AccessTypeEntity updatedEntity)
		{
			var accessType = GetById(updatedEntity.Id);
			if (!string.IsNullOrWhiteSpace(updatedEntity.Title))
				accessType.Title = updatedEntity.Title;

			base.Update(accessType);
		}

		public void Delete(int[] id)
		{
			var entities = GetByQuery(w => id.Contains(w.Id)).ToList();

			if (entities.Count > 0)
				base.Delete(entities);
		}
	}
}