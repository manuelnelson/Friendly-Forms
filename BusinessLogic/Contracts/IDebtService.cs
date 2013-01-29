using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IDebtService : IFormService<IDebtRepository, Debt>
    {
    }
}
