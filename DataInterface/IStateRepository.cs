using System.Collections.Generic;
using Models;

namespace DataInterface
{
    public interface IStateRepository : IRepository<State>
    {
        IEnumerable<State> GetAll();
    }
}
