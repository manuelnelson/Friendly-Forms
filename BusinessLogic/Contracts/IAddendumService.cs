using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IAddendumService : IFormService<IAddendumRepository, Addendum>
    {
    }
}
