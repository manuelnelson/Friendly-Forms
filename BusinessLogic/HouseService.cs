using BusinessLogic.Contracts;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class HouseService : FormService<IHouseRepository, House, HouseViewModel>, IHouseService
    {
        public HouseService(IHouseRepository formRepository) : base(formRepository)
        {
        }
    }
}
