using System.Collections.Generic;
using Models;

namespace DataInterface
{
    public interface IClientRepository : IRepository<Client>
    {
        IEnumerable<User> GetUsersClients(int userId);
    }
}
