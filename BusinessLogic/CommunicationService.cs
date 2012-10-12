using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class CommunicationService : FormService<CommunicationRepository,Communication,CommunicationViewModel>, ICommunicationService
    {
        public CommunicationService(CommunicationRepository formRepository) : base(formRepository)
        {
        }
    }
}
