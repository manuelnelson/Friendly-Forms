using System.Collections.Generic;
using System.Linq;
using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class CountyRepository : Repository<County>, ICountyRepository
    {
        public CountyRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<County> GetAll()
        {
            return GetDbSet().ToList();
        }
    }
}
