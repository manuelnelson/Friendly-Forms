using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IPrivacyService : IService<IPrivacyRepository, Privacy>
    {
    }
}
