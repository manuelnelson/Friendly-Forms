using BusinessLogic.Models;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IParticipantService : IFormService<IParticipantRepository,Participant>
    {
    }
}
