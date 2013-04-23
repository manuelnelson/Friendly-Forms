using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IDeviationsService : IFormService<IDeviationsRepository, Deviations>
    {
        Deviations GetByChildId(long childId, bool isOtherParent = false);
    }
}
