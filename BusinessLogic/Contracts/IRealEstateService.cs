using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IRealEstateService : IFormService<IRealEstateRepository, RealEstateAndProperty>
    {
    }
}
