using System;
using BusinessLogic.Contracts;
using BusinessLogic.Models;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class ParticipantService : FormService<IParticipantRepository, Participant, ParticipantViewModel>, IParticipantService
    {
        private IParticipantRepository ParticipantRepository { get; set; }
        public ParticipantService(IParticipantRepository formRepository) : base(formRepository)
        {
            ParticipantRepository = formRepository;
        }

        public CustodyInformation GetCustodyInformation(long userId)
        {
            var participant = ParticipantRepository.GetByUserId(userId);            
            return GetCustodyInformation(participant.ConvertToModel() as ParticipantViewModel);
        }

        public CustodyInformation GetCustodyInformation(ParticipantViewModel participant)
        {
            if (participant == null)
                throw new ArgumentNullException();
            //Parents
            if (participant.PlaintiffCustodialParent == (int)CustodialParent.Primary)
            {
                var custodyInformation = new CustodyInformation()
                    {
                        CustodyParent = Enum.GetName(typeof (ParentRelationship), participant.PlaintiffRelationship),
                        NonCustodyParent = Enum.GetName(typeof (ParentRelationship), participant.DefendantRelationship),                                                    
                        NonCustodyParentName = participant.DefendantsName,
                        CustodyParentName = participant.PlaintiffsName
                    };
                custodyInformation.NonCustodyIsFather = custodyInformation.NonCustodyParent == Enum.GetName(typeof(ParentRelationship), ParentRelationship.Father);
                custodyInformation.LegalCustodyPhrase = "The " + custodyInformation.CustodyParent + " will be the primary legal custodian of the children.";
                return custodyInformation;
            }
            if (participant.DefendantCustodialParent ==(int)CustodialParent.Primary)
            {
                var custodyInformation = new CustodyInformation()
                    {
                        CustodyParent = Enum.GetName(typeof (ParentRelationship), participant.DefendantRelationship),
                        NonCustodyParent = Enum.GetName(typeof (ParentRelationship), participant.PlaintiffRelationship),
                        NonCustodyParentName = participant.PlaintiffsName,
                        CustodyParentName = participant.DefendantsName
                    };
                custodyInformation.LegalCustodyPhrase = "The " + custodyInformation.CustodyParent + " will be the primary legal custodian of the children.";
                return custodyInformation;
            }
            //TODO: fix Joint Custody
            return new CustodyInformation()
                {
                    CustodyParent = "Both parents",
                    LegalCustodyPhrase = "The parties will share legal custody of the children."
                };
        }
    }
}
