using BusinessLogic.Contracts;
using DataInterface;
using Models;

namespace BusinessLogic
{
    public class DeviationsService : FormService<IDeviationsRepository, Deviations>, IDeviationsService
    {
        public DeviationsService(IDeviationsRepository formRepository)
            : base(formRepository)
        {
        }

        public Deviations GetByChildId(long childId, bool isOtherParent = false)
        {
            return FormRepository.GetChildById(childId, isOtherParent);
        }
    }
}
