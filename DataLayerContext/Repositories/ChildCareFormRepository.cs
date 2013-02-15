using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class ChildCareFormRepository : FormRepository<ChildCareForm>, IChildCareFormRepository
    {
        public ChildCareFormRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}