using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class ChildCareRepository : FormRepository<ChildCare>, IChildCareRepository
    {
        public ChildCareRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ChildCare GetChildById(long childId)
        {
            return GetDbSet().FirstOrDefault(c => c.ChildId == childId);
        }

        public IEnumerable<ChildCare> GetAllByUserId(long userId)
        {
            return GetDbSet().Where(c => c.UserId == userId).Include(x=>x.Child);
        }
    }
}