using BusinessLogic.Contracts;
using DataInterface;
using Models;

namespace BusinessLogic
{
    public class CourtService : FormService<ICourtRepository, Court>, ICourtService
    {
        public CourtService(ICourtRepository formRepository)
            : base(formRepository)
        {
        }
    }
}
