using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IInformationService : IFormService<IInformationRepository, Information>
    {
    }
}
