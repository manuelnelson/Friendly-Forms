using System.Collections.Generic;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IPreexistingSupportChildService : IFormService<IPreexistingSupportChildRepository, PreexistingSupportChild>
   {
        IEnumerable<PreexistingSupportChild> GetChildrenBySupportId(int id);
        PreexistingSupportChild AddOrUpdate(PreexistingSupportChildViewModel model);
   }
}
