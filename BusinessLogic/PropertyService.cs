using BusinessLogic.Contracts;
using DataInterface;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class PropertyService : FormService<IRealEstateRepository, Property, PropertyViewModel>, IPropertyService
    {
        public PropertyService(IRealEstateRepository formRepository)
            : base(formRepository)
        {
        }
    }
}
