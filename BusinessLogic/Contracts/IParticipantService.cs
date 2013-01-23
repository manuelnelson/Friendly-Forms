using BusinessLogic.Models;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IParticipantService : IService<IParticipantRepository,Participant>
    {
        CustodyInformation GetCustodyInformation(int userId);
        CustodyInformation GetCustodyInformation(ParticipantViewModel participant);
    }
}
