using System.Collections.Generic;
using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IChildCareService : IFormService<IChildCareRepository, ChildCare>
    {
        ChildCare GetByChildId(long childId);
        List<ChildCare> GetAllByUserId(long userId);
    }
}
