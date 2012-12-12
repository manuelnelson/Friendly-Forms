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
    [Route("/SocialSecurity/")]
    public class ReqSocialSecurity
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public bool IsOtherParent { get; set; }
        [DataMember]
        public int ReceiveSocial { get; set; }
        [DataMember]
        public string Amount { get; set; }
    }

    [DataContract]
    public class RespSocialSecurity : IHasResponseStatus
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class SocialSecurityRestService : Service
    {
        public ISocialSecurityService SocialSecurityService { get; set; }

        public object Post(ReqSocialSecurity request)
        {
            var socialSecurityViewModel = request.TranslateTo<SocialSecurityViewModel>();
            SocialSecurityService.AddOrUpdate(socialSecurityViewModel);
            return new RespSocialSecurity();
        }
    }
}