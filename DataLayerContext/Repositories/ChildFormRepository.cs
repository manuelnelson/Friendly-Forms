using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class ChildFormRepository : FormRepository<ChildForm>, IChildFormRepository
    {
        public ChildFormRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
