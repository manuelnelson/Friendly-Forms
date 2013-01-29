using System;
using BusinessLogic.Contracts;
using BusinessLogic.Models;
using DataLayerContext.Repositories;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class ParticipantService : FormService<ParticipantRepository, Participant, ParticipantViewModel>, IParticipantService
    {
        private ParticipantRepository ParticipantRepository { get; set; }
        public ParticipantService(ParticipantRepository formRepository) : base(formRepository)
        {
            ParticipantRepository = formRepository;
        }

        public CustodyInformation GetCustodyInformation(int userId)
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
                        Parent = Enum.GetName(typeof (ParentRelationship), participant.PlaintiffRelationship),
                        NonCustodyParent = Enum.GetName(typeof (ParentRelationship), participant.DefendantRelationship),                                                    
                        NonCustodyParentName = participant.DefendantsName,
                    };
                custodyInformation.NonCustodyIsFather = custodyInformation.NonCustodyParent == Enum.GetName(typeof(ParentRelationship), ParentRelationship.Father);
                custodyInformation.LegalCustodyPhrase = "The " + custodyInformation.Parent + " will be the primary legal custodian of the children";
                return custodyInformation;
            }
            if (participant.DefendantCustodialParent ==(int)CustodialParent.Primary)
            {
                var custodyInformation = new CustodyInformation()
                    {
                        Parent = Enum.GetName(typeof (ParentRelationship), participant.DefendantRelationship),
                        NonCustodyParent = Enum.GetName(typeof (ParentRelationship), participant.PlaintiffRelationship),
                        NonCustodyParentName = participant.PlaintiffsName,                        
                    };
                custodyInformation.LegalCustodyPhrase = "The " + custodyInformation.Parent + " will be the primary legal custodian of the children";
                return custodyInformation;
            }
            //TODO: fix Joint Custody
            return new CustodyInformation()
                {
                    Parent = "Both parents",
                    LegalCustodyPhrase = "The parties will share legal custody of the children"
                };
        }
    }
}
