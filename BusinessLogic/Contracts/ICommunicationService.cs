using DataLayerContext.Repositories;
using Models;

namespace BusinessLogic.Contracts
{
    public interface ICommunicationService : IService<CommunicationRepository, Communication>
    {
    }
}
