using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class LawFirmOrmLiteRepository : OrmLiteRepository<LawFirm>, ILawFirmRepository
    {
        public LawFirmOrmLiteRepository(IDbConnectionFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}
