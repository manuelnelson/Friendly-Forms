using System.Collections.Generic;
using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IChildService : IFormService<IChildRepository,Child>
    {
        new List<Child> GetByUserId(long userId);
    }
}
