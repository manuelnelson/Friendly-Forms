using System.Collections.Generic;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IOtherChildService : IFormService<IOtherChildRepository, OtherChild>
    {
        OtherChild AddOrUpdate(OtherChildViewModel model);
        IEnumerable<OtherChild> GetChildrenByOtherChildrenId(int otherChildrenId);
    }
}
