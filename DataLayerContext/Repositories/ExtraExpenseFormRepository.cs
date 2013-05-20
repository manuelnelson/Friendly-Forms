using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class ExtraExpenseFormRepository : FormRepository<ExtraExpenseForm>, IExtraExpenseFormRepository
    {
        public ExtraExpenseFormRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}