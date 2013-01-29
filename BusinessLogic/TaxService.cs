using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class TaxService : FormService<TaxRepository, Tax, TaxViewModel>, ITaxService
    {
        public TaxService(TaxRepository formRepository) : base(formRepository)
        {
        }
    }
}
