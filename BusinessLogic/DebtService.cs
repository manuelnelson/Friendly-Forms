using BusinessLogic.Contracts;
using DataInterface;
using Models;

namespace BusinessLogic
{
    public class DebtService : FormService<IDebtRepository, Debt>, IDebtService
    {
        public DebtService(IDebtRepository formRepository)
            : base(formRepository)
        {
        }
    }
}
