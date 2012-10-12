using BusinessLogic.Models;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Helpers
{
    public static class ParticipantHelper
    {
        public static ParticipantViewModel ConvertToModel(this Participant participant)
        {
            return new ParticipantViewModel()
                {
                    DefendantRelationship = participant.DefendantRelationship,
                    DefendantCustodialParent = participant.DefendantCustodialParent,
                    DefendantsName = participant.DefendantsName,
                    PlaintiffCustodialParent = participant.PlaintiffCustodialParent,
                    PlaintiffRelationship = participant.PlaintiffRelationship,
                    PlaintiffsName = participant.PlaintiffsName,
                    UserId = participant.UserId
                };
        }
    }
}
