using BusinessLogic.Contracts;
using DataInterface;
using Models;

namespace BusinessLogic
{
    public class AttorneyPageService : Service<IAttorneyPageRepository, AttorneyPage>, IAttorneyPageService
    {
        public AttorneyPageService(IAttorneyPageRepository repository) : base(repository)
        {
        }
    }
}
