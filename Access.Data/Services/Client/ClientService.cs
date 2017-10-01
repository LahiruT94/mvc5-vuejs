using Access.Data.DAL;
using Access.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Access.Data.Services
{
    public class ClientService : BaseService<ClientEntity>, IClientService
    {
        private readonly IRepository<AccessEntity> _accessRepository;

        public ClientService(IRepository<ClientEntity> repository, IRepository<AccessEntity> accessRepository)
            : base(repository)
        {
            this._accessRepository = accessRepository;
        }

        public new IEnumerable<AccessView> GetAll()
        {
            IQueryable<ClientEntity> clients = _repository.Table;
            IQueryable<AccessEntity> access = _accessRepository.Table.Include(w => w.AccessType);

            IQueryable<AccessView> fullinfo = from s in clients
                from p in s.ProjectList
                join a in access on p.Id equals a.ProjectId
                select new AccessView()
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

        public override void Update(ClientEntity updatedEntity)
        {
            ClientEntity client = base.GetById(updatedEntity.Id);
            client.Email = updatedEntity.Email;
            client.Note = updatedEntity.Note;
            client.Phone = updatedEntity.Phone;
            client.Title = updatedEntity.Title;

            base.Update(client);
        }


        public class AccessView
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Note { get; set; }
            public int ProjectId { get; set; }
            public string ProjectTitle { get; set; }
            public int AccessId { get; set; }
            public string AccessType { get; set; }
            public string Password { get; set; }
            public string Login { get; set; }
            public string Address { get; set; }
            public string AccessNote { get; set; }
        }
    }
}