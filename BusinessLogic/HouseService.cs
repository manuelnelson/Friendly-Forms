using BusinessLogic.Contracts;
using DataInterface;
using Models;

namespace BusinessLogic
{
    public class HouseService : FormService<IHouseRepository, House>, IHouseService
    {
        public HouseService(IHouseRepository formRepository) : base(formRepository)
        {
        }
    }
}
