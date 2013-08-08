using BusinessLogic.Contracts;
using DataInterface;
using Models;

namespace BusinessLogic
{
    public class PropertyService : FormService<IRealEstateRepository, Property>, IPropertyService
    {
        public PropertyService(IRealEstateRepository formRepository)
            : base(formRepository)
        {
        }
    }
}
