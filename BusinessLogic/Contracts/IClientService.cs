using System.Collections.Generic;
using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IClientService : IService<IClientRepository, Client>
    {
        List<User> GetUsersClients(long userId);
        bool LawyerHasClient(long lawyerId, int clientId);
    }
}

