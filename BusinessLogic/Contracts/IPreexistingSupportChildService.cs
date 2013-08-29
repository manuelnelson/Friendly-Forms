using System.Collections.Generic;
using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IPreexistingSupportChildService : IFormService<IPreexistingSupportChildRepository, PreexistingSupportChild>
   {
        IEnumerable<PreexistingSupportChild> GetChildrenBySupportId(long preexistingSupportId);
   }
}
