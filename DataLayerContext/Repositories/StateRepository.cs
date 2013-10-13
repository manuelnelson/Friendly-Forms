using System.Collections.Generic;
using System.Linq;
using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class StateRepository : Repository<State>, IStateRepository
    {
        public StateRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IEnumerable<State> GetAll()
        {
            return GetDbSet().ToList().OrderBy(x=>x.Name);
        }
    }
}
