using System.Collections.Generic;
using System.Linq;
using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class ChildRepository : FormRepository<Child>, IChildRepository
    {
        public ChildRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public new List<Child> GetByUserId(long userId)
        {
            return GetDbSet().Where(c => c.UserId.Equals(userId)).ToList();
        }
    }
}
