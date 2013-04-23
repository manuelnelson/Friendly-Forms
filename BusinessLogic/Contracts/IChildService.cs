using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IChildService : IFormService<IChildRepository,Child>
    {
        Child AddOrUpdate(ChildViewModel model);
        new ChildViewModel GetByUserId(long userId);
    }
}
