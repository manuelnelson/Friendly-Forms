using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class PropertyService : FormService<RealEstateRepository, Property, PropertyViewModel>, IPropertyService
    {
        public PropertyService(RealEstateRepository formRepository) : base(formRepository)
        {
        }
    }
}
