using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class AttorneyPageUserRepository : Repository<AttorneyPageUser>, IAttorneyPageUserRepository
    {
        public AttorneyPageUserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
