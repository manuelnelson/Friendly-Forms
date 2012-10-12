using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class RealEstateRepository : FormRepository<RealEstateAndProperty>, IRealEstateRepository
    {
        public RealEstateRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
