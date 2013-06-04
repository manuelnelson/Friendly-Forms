using BusinessLogic.Contracts;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class CommunicationService : FormService<ICommunicationRepository,Communication,CommunicationViewModel>, ICommunicationService
    {
        public CommunicationService(ICommunicationRepository formRepository) : base(formRepository)
        {
        }
    }
}
