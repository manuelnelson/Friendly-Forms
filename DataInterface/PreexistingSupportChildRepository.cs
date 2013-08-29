using System.Collections.Generic;
using Models;

namespace DataInterface
{
    public interface IPreexistingSupportChildRepository : IFormRepository<PreexistingSupportChild>
    {
        IEnumerable<PreexistingSupportChild> GetChildrenById(long preexistingSupportId);
    }
}
