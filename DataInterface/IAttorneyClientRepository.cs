using System.Collections.Generic;
using Models;

namespace DataInterface
{
    public interface IAttorneyClientRepository : IRepository<AttorneyClient>
    {
        IEnumerable<AttorneyClient> GetByUserId(long userId);
    }
}
