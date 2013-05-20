using System.Collections.Generic;
using Models;

namespace DataInterface
{
    public interface IExtraExpenseRepository : IFormRepository<ExtraExpense>
    {
        ExtraExpense GetChildById(long childId);
        IEnumerable<ExtraExpense> GetAllByUserId(long userId);
    }
}
