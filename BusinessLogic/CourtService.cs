using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class CourtService : Service<CourtRepository, Court, CourtViewModel>, ICourtService
    {
        public CourtService(CourtRepository formRepository) : base(formRepository)
        {
        }
    }
}
