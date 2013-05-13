using System;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
using ServiceStack.Common;
using ServiceStack.Common.Extensions;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/Information/")]
    public class ReqInformation
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public int InformationAccess { get; set; }
    }

    [DataContract]
    public class RespInformation : IHasResponseStatus
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
    [Authenticate]
    public class InformationRestService : ServiceBase
    {
        public IInformationService InformationService { get; set; }

        public object Post(ReqInformation request)
        {
            var information = request.TranslateTo<Information>();
            information.UserId = Convert.ToInt64(UserSession.Id);
            InformationService.Add(information);
            return new RespInformation()
                {
                    Id = information.Id
                };
        }
        public object Put(ReqInformation request)
        {
            var information = request.TranslateTo<Information>();
            information.UserId = Convert.ToInt64(UserSession.Id);
            InformationService.Update(information);
            return new RespInformation();
        }
    }
}