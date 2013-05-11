using System;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
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
        public long Id { get; set; }
        [DataMember]
        public long UserId { get; set; }
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
        public string LimitationDetails { get; set; }
        [DataMember]
        public int Notification { get; set; }
        [DataMember]
        public int AccessOfRights { get; set; }
        [DataMember]
        public string AccessOfRightsDetails { get; set; }
    }

    [DataContract]
    public class RespCommunication : IHasResponseStatus
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
    [Authenticate]
    public class CommunicationRestService : ServiceBase
    {
        public ICommunicationService CommunicationService{ get; set; }

        public object Post(ReqCommunication request)
        {
            var communication = request.TranslateTo<Communication>();
            communication.UserId = Convert.ToInt32(UserSession.CustomId);
            CommunicationService.Add(communication);
            return new RespCommunication()
                {
                    Id = communication.Id
                };
        }
        public object Put(ReqCommunication request)
        {
            var communication = request.TranslateTo<Communication>();
            communication.UserId = Convert.ToInt32(UserSession.CustomId);
            CommunicationService.Update(communication);
            return new RespCommunication();
        }
    }
}