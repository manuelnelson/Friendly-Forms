using BusinessLogic.Models;
using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IParticipantService : IFormService<IParticipantRepository,Participant>
    {
        CustodyInformation GetCustodyInformation(long userId);
        CustodyInformation GetCustodyInformation(Participant participant);
    }
}
