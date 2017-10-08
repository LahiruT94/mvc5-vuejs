using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Access.Data.DAL;
using Access.Data.Models;
using Access.Data.ViewModels;

namespace Access.Data.Services
{
	public class ClientService : BaseService<ClientEntity>, IClientService
	{
		private readonly IRepository<AccessEntity> _accessRepository;

		public ClientService(IRepository<ClientEntity> repository, IRepository<AccessEntity> accessRepository)
			: base(repository)
		{
			_accessRepository = accessRepository;
		}

		public override void Update(ClientEntity updatedEntity)
		{
			var client = GetById(updatedEntity.Id);
			client.Email = updatedEntity.Email;
			client.Note = updatedEntity.Note;
			client.Phone = updatedEntity.Phone;
			client.Title = updatedEntity.Title;

			base.Update(client);
		}

		public new IEnumerable<AccessListViewModel> GetAll()
		{
			var clients = Repository.Table;
			var access = _accessRepository.Table.Include(w => w.AccessType);

			var fullinfo = from s in clients
				from p in s.ProjectList
				join a in access on p.Id equals a.ProjectId
				select new AccessListViewModel
				{
					Id = a.Id,
					Title = s.Title,
					Phone = s.Phone,
					Email = s.Email,
					Note = s.Note,
					ProjectId = p.Id,
					ProjectTitle = p.Title,
					AccessId = a.Id,
					AccessType = a.AccessType.Title,
					Password = a.Password,
					Login = a.Login,
					Address = a.Address,
					AccessNote = a.Note
				};

			return fullinfo.AsEnumerable();
		}
	}
}