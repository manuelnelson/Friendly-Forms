using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class AddendumRepository : FormRepository<Addendum>, IAddendumRepository
    {
        public AddendumRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
