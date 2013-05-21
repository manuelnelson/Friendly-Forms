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
    [Route("/SocialSecurity/")]
    public class ReqSocialSecurity
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long UserId { get; set; }
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
    [Authenticate]
    public class SocialSecurityRestService : ServiceBase
    {
        public ISocialSecurityService SocialSecurityService { get; set; }
        public object Get(ReqSocialSecurity request)
        {
            if (request.Id != 0)
            {
                return SocialSecurityService.Get(request.Id);
            }
            return SocialSecurityService.GetByUserId(request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId));
        }
        public object Post(ReqSocialSecurity request)
        {
            var socialSecurity = request.TranslateTo<SocialSecurity>();
            socialSecurity.UserId = Convert.ToInt32(UserSession.CustomId);
            SocialSecurityService.Add(socialSecurity);
            return new RespSocialSecurity
                {
                    Id = socialSecurity.Id
                };
        }
        public object Put(ReqSocialSecurity request)
        {
            var socialSecurity = request.TranslateTo<SocialSecurity>();
            socialSecurity.UserId = Convert.ToInt32(UserSession.CustomId);
            SocialSecurityService.Update(socialSecurity);
            return new RespSocialSecurity();
        }
    }
}