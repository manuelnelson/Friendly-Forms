using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class HouseRepository : FormRepository<House>, IHouseRepository 
    {
        public HouseRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
