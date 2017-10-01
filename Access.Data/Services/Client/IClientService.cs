using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Access.Data.Models;

namespace Access.Data.Services
{
    public interface IClientService : IBaseService<ClientEntity>
    {
        new IEnumerable<ClientService.AccessView> GetAll();
        new void Update(ClientEntity updatedEntity);
    }
}
