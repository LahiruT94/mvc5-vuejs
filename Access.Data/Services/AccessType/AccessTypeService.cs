using System.Linq;
using Access.Data.DAL;
using Access.Data.Models;

namespace Access.Data.Services
{
	public class AccessTypeService : BaseService<AccessTypeEntity>, IAccessTypeService
	{
		public AccessTypeService(IRepository<AccessTypeEntity> repository)
			: base(repository)
		{
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