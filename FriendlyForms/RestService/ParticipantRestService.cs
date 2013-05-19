using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/Participant/")]
    public class ReqParticipant
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string PlaintiffsName { get; set; }
        [DataMember]
        public int PlaintiffRelationship { get; set; }
        [DataMember]
        public int PlaintiffCustodialParent { get; set; }
        [DataMember]
        public string DefendantsName { get; set; }
        [DataMember]
        public int DefendantRelationship { get; set; }
        [DataMember]
        public int DefendantCustodialParent { get; set; }
    }

    [DataContract]
    public class RespParticipant : IHasResponseStatus
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class ParticipantRestService : Service
    {
        public IParticipantService ParticipantService { get; set; }
        public object Get(ReqParticipant request)
        {
            if (request.Id != 0)
            {
                return ParticipantService.Get(request.Id);
            }
            if (request.UserId != 0)
            {
                return ParticipantService.GetByUserId(request.UserId);
            }
            return new Participant();
        }
        public object Post(ReqParticipant request)
        {
            var participant = request.TranslateTo<Participant>();
            ParticipantService.Add(participant);
            return new RespParticipant
                {
                    Id = participant.Id
                };
        }
        public object Put(ReqParticipant request)
        {
            var participant = request.TranslateTo<Participant>();
            ParticipantService.Update(participant);
            return new RespParticipant();
        }
    }
}