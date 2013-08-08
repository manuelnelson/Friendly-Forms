using BusinessLogic.Contracts;
using DataInterface;
using Models;

namespace BusinessLogic
{
    public class InformationService : FormService<IInformationRepository, Information>, IInformationService
    {
        public InformationService(IInformationRepository formRepository)
            : base(formRepository)
        {
        }
    }
}
