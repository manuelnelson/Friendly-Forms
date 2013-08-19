using System.Collections.Generic;
using Models;

namespace DataInterface
{
    public interface IDecisionRepository : IFormRepository<Decisions>
    {
        Decisions GetByChildId(long childId);
        List<Decisions> GetChildListByUserId(long userId);
    }
}
