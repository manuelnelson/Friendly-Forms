using System.Linq;
using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class ParticipantRepository : FormRepository<Participant>, IParticipantRepository
    {
        public ParticipantRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

    }
}
