using BusinessLogic.Contracts;
using DataInterface;
using Models;

namespace BusinessLogic
{
    public class PrivacyService : FormService<IPrivacyRepository, Privacy>, IPrivacyService
    {
        public PrivacyService(IPrivacyRepository formRepository)
            : base(formRepository)
        {
        }
    }
}
