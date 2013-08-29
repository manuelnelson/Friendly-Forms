using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class LawFirmRepository : Repository<LawFirm>, ILawFirmRepository
    {
        public LawFirmRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }

}
