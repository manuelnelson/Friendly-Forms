using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class SocialSecurityOrmLiteRepository : FormOrmLiteRepository<SocialSecurity>, ISocialSecurityRepository
    {
        public SocialSecurityOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
