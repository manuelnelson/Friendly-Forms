using BusinessLogic.Models;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IParticipantService : IFormService<IParticipantRepository,Participant>
    {
        CustodyInformation GetCustodyInformation(long userId);
        CustodyInformation GetCustodyInformation(ParticipantViewModel participant);
    }
}
