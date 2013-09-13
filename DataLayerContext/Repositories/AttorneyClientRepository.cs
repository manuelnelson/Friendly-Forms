using System.Collections.Generic;
using System.Linq;
using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class AttorneyClientRepository : Repository<AttorneyClient>, IAttorneyClientRepository
    {
        public AttorneyClientRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<AttorneyClient> GetByUserId(long userId)
        {
            return GetDbSet().Where(x => x.UserId == userId);
        }
    }
}