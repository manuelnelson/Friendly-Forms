using System.Linq;
using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class CourtRepository : FormRepository<Court>, ICourtRepository
    {
        public CourtRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

    }
}
