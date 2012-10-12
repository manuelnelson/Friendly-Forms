using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class SpecialCircumstancesRepository : FormRepository<SpecialCircumstances>, ISpecialCircumstancesRepository
    {
        public SpecialCircumstancesRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
