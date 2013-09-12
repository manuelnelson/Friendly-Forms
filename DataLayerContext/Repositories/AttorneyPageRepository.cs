using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class AttorneyPageRepository : Repository<AttorneyPage>, IAttorneyPageRepository
    {
        public AttorneyPageRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork){}
    }

}
