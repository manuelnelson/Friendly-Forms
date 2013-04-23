using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class DeviationsService : FormService<DeviationsRepository, Deviations, DeviationsViewModel>, IDeviationsService
    {
        public DeviationsService(DeviationsRepository formRepository) : base(formRepository)
        {
        }

        public Deviations GetByChildId(long childId, bool isOtherParent = false)
        {
            return FormRepository.GetChildById(childId, isOtherParent);
        }
    }
}
