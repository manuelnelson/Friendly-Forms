using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class IncomeRepository : FormRepository<Income>, IIncomeRepository
    {
        public IncomeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
