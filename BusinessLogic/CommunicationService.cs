using BusinessLogic.Contracts;
using DataInterface;
using Models;

namespace BusinessLogic
{
    public class CommunicationService : FormService<ICommunicationRepository, Communication>, ICommunicationService
    {
        public CommunicationService(ICommunicationRepository formRepository) : base(formRepository)
        {
        }
    }
}
