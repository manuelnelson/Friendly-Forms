using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class ParticipantService : FormService<ParticipantRepository, Participant, ParticipantViewModel>, IParticipantService
    {
        public ParticipantService(ParticipantRepository formRepository) : base(formRepository)
        {
        }
    }
}
