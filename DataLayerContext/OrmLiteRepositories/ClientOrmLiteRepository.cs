using System.Collections.Generic;
using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class ClientOrmLiteRepository : OrmLiteRepository<Client>, IClientRepository
    {
        public ClientOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<User> GetUsersClients(long userId)
        {
            throw new System.NotImplementedException();
        }

        public bool LawyerHasClient(long lawyerId, int clientId)
        {
            throw new System.NotImplementedException();
        }
    }
}
