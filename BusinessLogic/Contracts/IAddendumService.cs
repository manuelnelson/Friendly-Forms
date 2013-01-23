using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IAddendumService : IService<IAddendumRepository, Addendum>
    {
    }
}
