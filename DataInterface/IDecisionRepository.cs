using Models;

namespace DataInterface
{
    public interface IDecisionRepository : IFormRepository<Decisions>
    {
        Decisions GetByChildId(int childId);
    }
}
