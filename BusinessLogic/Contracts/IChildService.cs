using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IChildService : IFormService<IChildRepository,Child>
    {
        Child AddOrUpdate(ChildrenViewModel model);
        new ChildrenViewModel GetByUserId(int userId);
    }
}
