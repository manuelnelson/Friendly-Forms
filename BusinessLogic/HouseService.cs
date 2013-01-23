using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class HouseService : Service<HouseRepository, House, HouseViewModel>, IHouseService
    {
        public HouseService(HouseRepository formRepository) : base(formRepository)
        {
        }
    }
}
