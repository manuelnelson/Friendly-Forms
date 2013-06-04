using BusinessLogic.Contracts;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class InformationService : FormService<IInformationRepository, Information, InformationViewModel>, IInformationService
    {
        public InformationService(IInformationRepository formRepository)
            : base(formRepository)
        {
        }
    }
}
