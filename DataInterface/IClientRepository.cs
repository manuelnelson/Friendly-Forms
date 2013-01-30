using System.Collections.Generic;
using Models;

namespace DataInterface
{
    public interface IClientRepository : IRepository<Client>
    {
        IEnumerable<User> GetUsersClients(int userId);
        bool LawyerHasClient(int lawyerId, int clientId);
    }
}
