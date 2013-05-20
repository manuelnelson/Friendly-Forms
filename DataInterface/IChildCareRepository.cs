using System.Collections.Generic;
using Models;

namespace DataInterface
{
    public interface IChildCareRepository : IFormRepository<ChildCare>
    {
        ChildCare GetChildById(long childId);
        IEnumerable<ChildCare> GetAllByUserId(long userId);
    }
}
