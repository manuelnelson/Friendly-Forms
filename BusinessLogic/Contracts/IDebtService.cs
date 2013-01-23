using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IDebtService : IService<IDebtRepository, Debt>
    {
    }
}
