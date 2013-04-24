using System.Collections.Generic;
using Models;

namespace DataInterface
{
    public interface IChildCareRepository : IFormRepository<ChildCare>
    {
        ChildCare GetChildById(int childId);
        IEnumerable<ChildCare> GetAllByUserId(long userId);
    }
}
