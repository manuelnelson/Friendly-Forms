using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
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
        public long Id { get; set; }
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public int NeedPrivacy { get; set; }
        [DataMember]
        public int NeedSupervision { get; set; }
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
        public long Id { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class PrivacyRestService : Service
    {
        public IPrivacyService PrivacyService { get; set; }

        public object Post(ReqPrivacy request)
        {
            var privacy = request.TranslateTo<Privacy>();
            PrivacyService.Add(privacy);
            return new RespPrivacy()
                {
                    Id = privacy.Id
                };
        }
        public object Put(ReqPrivacy request)
        {
            var privacy = request.TranslateTo<Privacy>();
            PrivacyService.Update(privacy);
            return new RespPrivacy();
        }
    }
}