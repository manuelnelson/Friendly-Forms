using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IChildFormService : IService<IChildFormRepository, ChildForm>
    {
    }
}
