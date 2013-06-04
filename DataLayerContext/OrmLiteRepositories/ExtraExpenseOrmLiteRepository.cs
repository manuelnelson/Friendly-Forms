using System.Collections.Generic;
using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class ExtraExpenseOrmLiteRepository : FormOrmLiteRepository<ExtraExpense>, IExtraExpenseRepository
    {
        public ExtraExpenseOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }

        public ExtraExpense GetChildById(long childId)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                return db.First<ExtraExpense>(x => x.ChildId == childId);
            }  
        }

        public IEnumerable<ExtraExpense> GetAllByUserId(long userId)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                return db.Select<ExtraExpense>(x => x.UserId == userId);
            }
        }
    }
}
