using BusinessLogic.Contracts;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class AddendumService : FormService<IAddendumRepository, Addendum, AddendumViewModel>, IAddendumService
    {
        public AddendumService(IAddendumRepository formRepository) : base(formRepository)
        {
        }
    }
}
