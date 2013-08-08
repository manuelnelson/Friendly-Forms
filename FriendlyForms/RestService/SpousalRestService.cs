using System;
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
    [Route("/Spousals/")]
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
        public object Get(ReqSpousal request)
        {
            if (request.Id != 0)
            {
                return SpousalService.Get(request.Id);
            }
            return SpousalService.GetByUserId(request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId));
        }
        public object Post(ReqSpousal request)
        {
            var spousalSupport = request.TranslateTo<SpousalSupport>();
            spousalSupport.UserId = Convert.ToInt32(UserSession.CustomId);
            SpousalService.Add(spousalSupport);
            return new RespSpousal()
                {
                    Id = spousalSupport.Id
                };
        }
        public object Put(ReqSpousal request)
        {
            var spousalSupport = request.TranslateTo<SpousalSupport>();
            spousalSupport.UserId = Convert.ToInt32(UserSession.CustomId);
            SpousalService.Update(spousalSupport);
            return new RespSpousal();
        }
    }
}