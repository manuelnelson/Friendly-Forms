using System.Collections.Generic;
using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IAttorneyClientService : IService<IAttorneyClientRepository, AttorneyClient>
    {
        List<AttorneyClient> GetByUserId(long userId);
    }
}
