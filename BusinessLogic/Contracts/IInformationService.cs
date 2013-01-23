using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IInformationService : IService<IInformationRepository, Information>
    {
    }
}
