using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class DebtRepository : FormRepository<Debt>, IDebtRepository
    {
        public DebtRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
