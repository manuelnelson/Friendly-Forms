using BusinessLogic.Contracts;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class CourtService : FormService<ICourtRepository, Court, CourtViewModel>, ICourtService
    {
        public CourtService(ICourtRepository formRepository)
            : base(formRepository)
        {
        }
    }
}
