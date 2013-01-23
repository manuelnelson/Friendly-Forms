using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IChildService : IService<IChildRepository,Child>
    {
        Child AddOrUpdate(ChildViewModel model);
        new ChildViewModel GetByUserId(int userId);
    }
}
