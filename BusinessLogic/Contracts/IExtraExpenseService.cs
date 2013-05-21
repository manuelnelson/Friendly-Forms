using System.Collections.Generic;
using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IExtraExpenseService : IFormService<IExtraExpenseRepository, ExtraExpense>
    {
        ExtraExpense GetByChildId(long childId);
        List<ExtraExpense> GetAllByUserId(long userId);
    }
}
