using System.Linq;
using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class InformationRepository : FormRepository<Information>,IInformationRepository
    {
        public InformationRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
