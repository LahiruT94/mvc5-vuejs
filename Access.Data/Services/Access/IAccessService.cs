using Access.Data.Models;

namespace Access.Data.Services
{
	public interface IAccessService : IBaseService<AccessEntity>
	{
		new void Update(AccessEntity updatedEntity);
	}
}