using Access.Data.Models;

namespace Access.Data.Services
{
	public interface IAccessTypeService : IBaseService<AccessTypeEntity>
	{
		new void Update(AccessTypeEntity model);
		void Delete(int[] id);
	}
}