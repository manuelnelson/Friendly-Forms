using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class SocialSecurityRepository : FormRepository<SocialSecurity>, ISocialSecurityRepository
    {
        public SocialSecurityRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
