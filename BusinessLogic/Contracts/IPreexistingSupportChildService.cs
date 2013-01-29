using System.Collections.Generic;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IPreexistingSupportChildService : IFormService<IPreexistingSupportChildRepository, PreexistingSupportChild>
   {
        IEnumerable<PreexistingSupportChild> GetChildrenBySupportId(long id);
        PreexistingSupportChild AddOrUpdate(PreexistingSupportChildViewModel model);
   }
}
