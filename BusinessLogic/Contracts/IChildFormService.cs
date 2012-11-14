using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IChildFormService : IFormService<IChildFormRepository, ChildForm>
    {
    }
}
