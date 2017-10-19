using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Access.Data.DAL;
using Access.Data.Models;
using Access.Data.ViewModels;
using PagedList;

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

		public IPagedList<ClientItem> Get(Filter filter)
		{
			var result = Repository.TableNoTracking.Select(a => new ClientItem
			{
				Id = a.Id,
				Title = a.Title,
				Email = a.Email,
				Phone = a.Phone,
				Note = a.Note
			});

			if (filter == null)
				return result.OrderBy(w => w.Id).ToPagedList(1, 10);

			if (!string.IsNullOrWhiteSpace(filter.Search))
				result = result.Where(m => m.Id.ToString().Contains(filter.Search) ||
				                           m.Title.Contains(filter.Search) ||
				                           m.Email.Contains(filter.Search) ||
				                           m.Note.Contains(filter.Search) ||
				                           m.Phone.Contains(filter.Search));

			switch (filter.SortColumn)
			{
				case "Title":
				{
					result = filter.SortOrder == "descending" ? result.OrderByDescending(w => w.Title) : result.OrderBy(w => w.Title);
					break;
				}
				case "Email":
				{
					result = filter.SortOrder == "descending" ? result.OrderByDescending(w => w.Email) : result.OrderBy(w => w.Email);
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

		public override void Update(ClientEntity updatedEntity)
		{
			var client = GetById(updatedEntity.Id);
			client.Email = updatedEntity.Email;
			client.Note = updatedEntity.Note;
			client.Phone = updatedEntity.Phone;
			client.Title = updatedEntity.Title;

			base.Update(client);
		}

		public void Delete(int[] ids)
		{
			var entities = GetByQuery(w => ids.Contains(w.Id)).ToList();

			if (entities.Count > 0)
				base.Delete(entities);
		}

		public IEnumerable<AccessListViewModel> GetAll(int page, int pageSize, string filter, string orderKey)
		{
			var clients = Repository.TableNoTracking;
			var access = _accessRepository.TableNoTracking.Include(w => w.AccessType);

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

			if (!string.IsNullOrWhiteSpace(filter))
				fullinfo = fullinfo.Where(m => m.ToString().Contains(filter));


			return fullinfo.ToPagedList(page, pageSize);
		}
	}
}