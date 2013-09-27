using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class PreexistingSupportChildRepository : FormRepository<PreexistingSupportChild>, IPreexistingSupportChildRepository
    {
        public PreexistingSupportChildRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<PreexistingSupportChild> GetChildrenById(long preexistingSupportId)
        {
            return GetDbSet().Where(p => p.PreexistingSupportId == preexistingSupportId).Include(x=>x.PreexistingSupport);
        }

        public void DeleteChildrenBySupportId(int preexistingSupportId)
        {
            var list = GetDbSet().Where(x => x.PreexistingSupportId == preexistingSupportId);
            foreach (var preexistingSupportChild in list)
            {
                GetDbSet().Remove(preexistingSupportChild);
            }
            UnitOfWork.Commit();
        }
    }
}
