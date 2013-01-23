using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class PrivacyService : Service<PrivacyRepository, Privacy, PrivacyViewModel>, IPrivacyService
    {
        public PrivacyService(PrivacyRepository formRepository) : base(formRepository)
        {
        }
    }
}
