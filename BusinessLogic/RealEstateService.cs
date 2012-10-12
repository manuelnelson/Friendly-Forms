using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class RealEstateService : FormService<RealEstateRepository, RealEstateAndProperty, RealEstateViewModel>, IRealEstateService
    {
        public RealEstateService(RealEstateRepository formRepository) : base(formRepository)
        {
        }
    }
}
