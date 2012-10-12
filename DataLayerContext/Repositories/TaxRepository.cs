using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class TaxRepository : FormRepository<Tax>, ITaxRepository
    {
        public TaxRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
