using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class RealEstateRepository : FormRepository<Property>, IRealEstateRepository
    {
        public RealEstateRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
