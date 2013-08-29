using System.Collections.Generic;
using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IOtherChildService : IFormService<IOtherChildRepository, OtherChild>
    {
        IEnumerable<OtherChild> GetChildrenByOtherChildrenId(long otherChildrenId);
    }
}
