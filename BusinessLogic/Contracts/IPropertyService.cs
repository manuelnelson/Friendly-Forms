using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IPropertyService : IService<IRealEstateRepository, Property>
    {
    }
}
