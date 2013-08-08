using BusinessLogic.Contracts;
using DataInterface;
using Models;

namespace BusinessLogic
{
    public class TaxService : FormService<ITaxRepository, Tax>, ITaxService
    {
        public TaxService(ITaxRepository formRepository)
            : base(formRepository)
        {
        }
    }
}
