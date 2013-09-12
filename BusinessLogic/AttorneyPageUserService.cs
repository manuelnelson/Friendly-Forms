using BusinessLogic.Contracts;
using DataInterface;
using Models;

namespace BusinessLogic
{
    public class AttorneyPageUserService : Service<IAttorneyPageUserRepository, AttorneyPageUser>, IAttorneyPageUserService
    {
        public AttorneyPageUserService(IAttorneyPageUserRepository repository) : base(repository)
        {
        }
    }
}
