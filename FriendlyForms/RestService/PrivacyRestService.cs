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
    [Route("/Privacy/")]
    public class ReqPrivacy
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int NeedPrivacy { get; set; }
        [DataMember]
        public string Details { get; set; }
        [DataMember]
        public bool FatherSupervision { get; set; }
        [DataMember]
        public bool MotherSupervision { get; set; }
        [DataMember]
        public string SupervisionHow { get; set; }
        [DataMember]
        public string SupervisionWhere { get; set; }
        [DataMember]
        public string SupervisionWho { get; set; }
        [DataMember]
        public int? SupervisionCost { get; set; }

    }

    [DataContract]
    public class RespPrivacy : IHasResponseStatus
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class PrivacyRestService : Service
    {
        public IPrivacyService PrivacyService { get; set; }

        public object Post(ReqPrivacy request)
        {
            var privacyViewModel = request.TranslateTo<PrivacyViewModel>();
            PrivacyService.AddOrUpdate(privacyViewModel);
            return new RespPrivacy();
        }
    }
}