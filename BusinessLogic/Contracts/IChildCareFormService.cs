using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IChildCareFormService : IFormService<IChildCareFormRepository, ChildCareForm>
    {
    }
}
