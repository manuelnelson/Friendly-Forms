using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class CommunicationRepository : FormRepository<Communication>, ICommunicationRepository
    {
        public CommunicationRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

    }
}
