using BusinessLogic.Contracts;
using DataInterface;
using Models;

namespace BusinessLogic
{
    public class AddendumService : FormService<IAddendumRepository, Addendum>, IAddendumService
    {
        public AddendumService(IAddendumRepository formRepository) : base(formRepository)
        {
        }
    }
}
