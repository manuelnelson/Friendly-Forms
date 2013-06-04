using BusinessLogic.Contracts;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class PrivacyService : FormService<IPrivacyRepository, Privacy, PrivacyViewModel>, IPrivacyService
    {
        public PrivacyService(IPrivacyRepository formRepository)
            : base(formRepository)
        {
        }
    }
}
