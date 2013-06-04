using BusinessLogic.Contracts;
using DataInterface;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class TaxService : FormService<ITaxRepository, Tax, TaxViewModel>, ITaxService
    {
        public TaxService(ITaxRepository formRepository)
            : base(formRepository)
        {
        }
    }
}
