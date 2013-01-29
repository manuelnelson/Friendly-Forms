using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IPropertyService : IFormService<IRealEstateRepository, Property>
    {
    }
}
