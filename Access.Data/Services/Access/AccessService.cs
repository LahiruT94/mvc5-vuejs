using Access.Data.DAL;
using Access.Data.Models;

namespace Access.Data.Services
{
	public class AccessService : BaseService<AccessEntity>, IAccessService
	{
		public AccessService(IRepository<AccessEntity> repository)
			: base(repository)
		{
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
	}
}