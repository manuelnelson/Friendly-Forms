using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IPrivacyService : IFormService<IPrivacyRepository, Privacy>
    {
    }
}
