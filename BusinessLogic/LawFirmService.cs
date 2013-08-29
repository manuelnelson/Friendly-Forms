using BusinessLogic.Contracts;
using DataInterface;
using Models;

namespace BusinessLogic
{
    public class LawFirmService : Service<ILawFirmRepository, LawFirm>, ILawFirmService
    {
        public LawFirmService(ILawFirmRepository repository) : base(repository)
        {
        }
    }
}
