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
    [Route("/Spousal/")]
    public class ReqSpousal
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int Spousal { get; set; }
        [DataMember]
        public string SpousalDescription { get; set; }
    }

    [DataContract]
    public class RespSpousal : IHasResponseStatus
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class SpousalRestService : Service
    {
        public ISpousalService SpousalService { get; set; }

        public object Post(ReqSpousal request)
        {
            var spousalViewModel = request.TranslateTo<SpousalViewModel>();
            SpousalService.AddOrUpdate(spousalViewModel);
            return new RespSpousal();
        }
    }
}