using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IIncomeService : IFormService<IIncomeRepository, Income>
    {
        Income GetByUserId(long userId, bool isOtherParent = false);
        Income AddOrUpdate(IncomeViewModel model);
    }
}
