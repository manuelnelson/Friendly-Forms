using System.Linq;
using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class ResponsibilityRepository : FormRepository<Responsibility>, IResponsibilityRepository
    {
        public ResponsibilityRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
