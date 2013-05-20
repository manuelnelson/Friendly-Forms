using System.Collections.Generic;
using Models;

namespace DataInterface
{
    public interface IClientRepository : IRepository<Client>
    {
        IEnumerable<User> GetUsersClients(long userId);
        bool LawyerHasClient(long lawyerId, int clientId);
    }
}
