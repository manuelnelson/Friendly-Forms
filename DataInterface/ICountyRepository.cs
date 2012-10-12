using System.Collections.Generic;
using Models;

namespace DataInterface
{
    public interface ICountyRepository : IRepository<County>
    {
        IEnumerable<County> GetAll();
    }
}
