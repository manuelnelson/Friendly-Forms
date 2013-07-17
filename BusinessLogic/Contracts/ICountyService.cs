using System.Collections.Generic;
using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface ICountyService : IService<ICountyRepository, County>
    {
        IEnumerable<County> GetAll();        
    }
}
