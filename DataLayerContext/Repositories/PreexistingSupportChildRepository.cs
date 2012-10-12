using System.Collections.Generic;
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

        public IEnumerable<PreexistingSupportChild> GetChildrenById(int id)
        {
            return GetDbSet().Where(p => p.PreexistingSupportId.Equals(id));
        }
    }
}
