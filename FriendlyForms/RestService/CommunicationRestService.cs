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
    [Route("/Communication/")]
    public class ReqCommunication
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int AllowCommunication { get; set; }
        [DataMember]
        public bool Telephone { get; set; }
        [DataMember]
        public bool Email { get; set; }
        [DataMember]
        public bool Other { get; set; }
        [DataMember]
        public string OtherMethod { get; set; }
        [DataMember]
        public int Limitations { get; set; }
        [DataMember]
        public string FatherCommunicate { get; set; }
        [DataMember]
        public string MotherCommunicate { get; set; }
    }

    [DataContract]
    public class RespCommunication : IHasResponseStatus
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class CommunicationRestService : Service
    {
        public ICommunicationService CommunicationService{ get; set; }

        public object Post(ReqCommunication request)
        {
            var communicationViewModel = request.TranslateTo<CommunicationViewModel>();
            CommunicationService.AddOrUpdate(communicationViewModel);
            return new RespCommunication();
        }
    }
}