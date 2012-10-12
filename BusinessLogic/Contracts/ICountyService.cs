using System.Collections.Generic;
using Models;

namespace BusinessLogic.Contracts
{
    public interface ICountyService
    {
        IEnumerable<County> GetAll();
    }
}
