using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
using Models.ViewModels;
using ServiceStack.Common;
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
        public long Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public bool IsOtherParent { get; set; }
        [DataMember]
        public int ReceiveSocial { get; set; }
        [DataMember]
        public int? Amount { get; set; }
    }

    [DataContract]
    public class RespSocialSecurity : IHasResponseStatus
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class SocialSecurityRestService : Service
    {
        public ISocialSecurityService SocialSecurityService { get; set; }
        public object Get(ReqSocialSecurity request)
        {
            if (request.Id != 0)
            {
                return SocialSecurityService.Get(request.Id);
            }
            if (request.UserId != 0)
            {
                return SocialSecurityService.GetByUserId(request.UserId);
            }
            return new SocialSecurity();
        }
        public object Post(ReqSocialSecurity request)
        {
            var socialSecurity = request.TranslateTo<SocialSecurity>();
            SocialSecurityService.Add(socialSecurity);
            return new RespSocialSecurity
                {
                    Id = socialSecurity.Id
                };
        }
        public object Put(ReqSocialSecurity request)
        {
            var socialSecurity = request.TranslateTo<SocialSecurity>();
            SocialSecurityService.Update(socialSecurity);
            return new RespSocialSecurity();
        }
    }
}