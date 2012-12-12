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
    [Route("/Information/")]
    public class ReqInformation
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int InformationAccess { get; set; }
    }

    [DataContract]
    public class RespInformation : IHasResponseStatus
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class InformationRestService : Service
    {
        public IInformationService InformationService { get; set; }

        public object Post(ReqInformation request)
        {
            var informationViewModel = request.TranslateTo<InformationViewModel>();
            InformationService.AddOrUpdate(informationViewModel);
            return new RespInformation();
        }
    }
}