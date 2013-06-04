using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class ParticipantOrmLiteRepository : FormOrmLiteRepository<Participant>, IParticipantRepository
    {
        public ParticipantOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
