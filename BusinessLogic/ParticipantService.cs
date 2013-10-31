using System;
using System.Collections.Generic;
using BusinessLogic.Contracts;
using BusinessLogic.Models;
using DataInterface;
using Models;
using Models.Helper;

namespace BusinessLogic
{
    public class ParticipantService : FormService<IParticipantRepository, Participant>, IParticipantService
    {
        private IParticipantRepository ParticipantRepository { get; set; }
        public ParticipantService(IParticipantRepository formRepository) : base(formRepository)
        {
            ParticipantRepository = formRepository;
        }

        public CustodyInformation GetCustodyInformation(long userId)
        {
            var participant = ParticipantRepository.GetByUserId(userId);            
            return GetCustodyInformation(participant);
        }

        public CustodyInformation GetCustodyInformation(Participant participant)
        {
            if (participant == null)
                throw new ArgumentNullException();
            //Parents
            if (participant.PlaintiffCustodialParent == (int)CustodialParent.Primary)
            {
                var custodyInformation = new CustodyInformation
                    {
                        CustodyParent = Enum.GetName(typeof (ParentRelationship), participant.PlaintiffRelationship),
                        NonCustodyParent = Enum.GetName(typeof (ParentRelationship), participant.DefendantRelationship),                                                    
                        NonCustodyParentName = participant.DefendantsName,
                        CustodyParentName = participant.PlaintiffsName
                    };
                custodyInformation.NonCustodyIsFather = custodyInformation.NonCustodyParent == Enum.GetName(typeof(ParentRelationship), ParentRelationship.Father);
                custodyInformation.LegalCustodyPhrase = "The " + custodyInformation.CustodyParent + " will be the primary legal custodian of the children.";
                custodyInformation.CustodianNames = new List<string>
                    {
                        participant.DefendantsName,
                        participant.PlaintiffsName,                        
                    };
                return custodyInformation;
            }
            if (participant.DefendantCustodialParent ==(int)CustodialParent.Primary)
            {
                var custodyInformation = new CustodyInformation
                    {
                        CustodyParent = Enum.GetName(typeof (ParentRelationship), participant.DefendantRelationship),
                        NonCustodyParent = Enum.GetName(typeof (ParentRelationship), participant.PlaintiffRelationship),
                        NonCustodyParentName = participant.PlaintiffsName,
                        CustodyParentName = participant.DefendantsName
                    };
                custodyInformation.NonCustodyIsFather = custodyInformation.NonCustodyParent == Enum.GetName(typeof(ParentRelationship), ParentRelationship.Father);
                custodyInformation.LegalCustodyPhrase = "The " + custodyInformation.CustodyParent + " will be the primary legal custodian of the children.";
                custodyInformation.CustodianNames = new List<string>
                    {
                        participant.DefendantsName,
                        participant.PlaintiffsName,                        
                    };

                return custodyInformation;
            }
            return new CustodyInformation()
                {
                    CustodyParent = "Both parents",
                    LegalCustodyPhrase = "The parties will share legal custody of the children.",
                };
        }
    }
}
