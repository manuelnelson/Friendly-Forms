using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IIncomeService : IFormService<IIncomeRepository, Income>
    {
        Income GetByUserId(long userId, bool isOtherParent = false);
        bool HasNonW2Income(long userId);
    }
}
