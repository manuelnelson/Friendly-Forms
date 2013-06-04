using BusinessLogic.Contracts;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class DebtService : FormService<IDebtRepository, Debt, DebtViewModel>, IDebtService
    {
        public DebtService(IDebtRepository formRepository)
            : base(formRepository)
        {
        }
    }
}
