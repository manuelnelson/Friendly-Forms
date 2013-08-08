using BusinessLogic.Contracts;
using DataInterface;
using Models;

namespace BusinessLogic
{
    public class SpousalService : FormService<ISpousalRepository, SpousalSupport>, ISpousalService
    {
        public SpousalService(ISpousalRepository formRepository)
            : base(formRepository)
        {
        }
    }
}
