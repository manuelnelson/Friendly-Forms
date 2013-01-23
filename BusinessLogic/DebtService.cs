using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class DebtService : Service<DebtRepository, Debt, DebtViewModel>, IDebtService
    {
        public DebtService(DebtRepository formRepository) : base(formRepository)
        {
        }
    }
}
