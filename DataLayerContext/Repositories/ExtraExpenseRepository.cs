using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class ExtraExpenseRepository : FormRepository<ExtraExpense>, IExtraExpenseRepository
    {
        public ExtraExpenseRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ExtraExpense GetChildById(long childId)
        {
            return GetDbSet().FirstOrDefault(c => c.ChildId == childId);
        }

        public IEnumerable<ExtraExpense> GetAllByUserId(long userId)
        {
            return GetDbSet().Where(c => c.UserId == userId).Include(x => x.Child);
        }
    }
}