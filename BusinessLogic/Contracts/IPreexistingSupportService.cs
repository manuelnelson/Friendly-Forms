using System.Collections.Generic;
using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IPreexistingSupportService : IFormService<IPreexistingSupportRepository,PreexistingSupport>
    {
        List<PreexistingSupport> GetByUserId(long userId, bool isOtherParent = false);
    }
}
