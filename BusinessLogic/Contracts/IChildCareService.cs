using System.Collections.Generic;
using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IChildCareService : IFormService<IChildCareRepository, ChildCare>
    {
        ChildCare GetByChildId(int childId);
        List<ChildCare> GetAllByUserId(long userId);
    }
}
