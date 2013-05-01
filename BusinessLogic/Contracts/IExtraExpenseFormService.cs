using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IExtraExpenseFormService : IFormService<IExtraExpenseFormRepository, ExtraExpenseForm>
    {
    }
}
