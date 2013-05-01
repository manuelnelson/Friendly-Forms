using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class ExtraExpenseFormRepository : Repository<ExtraExpenseForm>, IExtraExpenseFormRepository
    {
        public ExtraExpenseFormRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}