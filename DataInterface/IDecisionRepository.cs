using Models;

namespace DataInterface
{
    public interface IDecisionRepository : IFormRepository<Decisions>
    {
        Decisions GetByChildId(long childId);
    }
}
