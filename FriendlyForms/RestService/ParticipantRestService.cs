using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models.ViewModels;
using ServiceStack.Common.Extensions;
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
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class ParticipantRestService : Service
    {
        public IParticipantService ParticipantService { get; set; }

        public object Post(ReqParticipant request)
        {
            var participantViewModel = request.TranslateTo<ParticipantViewModel>();
            ParticipantService.AddOrUpdate(participantViewModel);
            return new RespParticipant();
        }
    }
}