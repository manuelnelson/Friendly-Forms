using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class PrivacyRepository : FormRepository<Privacy>, IPrivacyRepository
    {
        public PrivacyRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
