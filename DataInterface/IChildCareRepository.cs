using Models;

namespace DataInterface
{
    public interface IChildCareRepository : IFormRepository<ChildCare>
    {
        ChildCare GetChildById(int childId);
    }
}
