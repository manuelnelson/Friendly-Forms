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
    [Route("/Spousal/")]
    public class ReqSpousal
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public int Spousal { get; set; }
        [DataMember]
        public string SpousalDescription { get; set; }
    }

    [DataContract]
    public class RespSpousal : IHasResponseStatus
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
    [Authenticate]
    public class SpousalRestService : ServiceBase
    {
        public ISpousalService SpousalService { get; set; }

        public object Post(ReqSpousal request)
        {
            var spousalSupport = request.TranslateTo<SpousalSupport>();
            spousalSupport.UserId = Convert.ToInt64(UserSession.Id);
            SpousalService.Add(spousalSupport);
            return new RespSpousal()
                {
                    Id = spousalSupport.Id
                };
        }
        public object Put(ReqSpousal request)
        {
            var spousalSupport = request.TranslateTo<SpousalSupport>();
            spousalSupport.UserId = Convert.ToInt64(UserSession.Id);
            SpousalService.Update(spousalSupport);
            return new RespSpousal();
        }
    }
}