using System.Collections.Generic;
using Models;

namespace DataInterface
{
    public interface IChildRepository : IFormRepository<Child>
    {
        new List<Child> GetByUserId(int userId);
    }
}
