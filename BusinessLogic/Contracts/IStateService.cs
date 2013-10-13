using System.Collections.Generic;
using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IStateService : IService<IStateRepository, State>
    {
        IEnumerable<State> GetAll();
    }
}
