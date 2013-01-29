using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class AddendumService : FormService<AddendumRepository, Addendum, AddendumViewModel>, IAddendumService
    {
        public AddendumService(AddendumRepository formRepository) : base(formRepository)
        {
        }
    }
}
