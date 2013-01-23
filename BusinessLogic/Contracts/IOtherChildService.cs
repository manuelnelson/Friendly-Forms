using System.Collections.Generic;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IOtherChildService : IService<IOtherChildRepository, OtherChild>
    {
        OtherChild AddOrUpdate(OtherChildViewModel model);
        IEnumerable<OtherChild> GetChildrenByOtherChildrenId(long otherChildrenId);
    }
}
