using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
using ServiceStack.Common.Extensions;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/Privacies/")]
    public class ReqPrivacy
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
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
        public object Get(ReqPrivacy request)
        {
            if (request.Id != 0)
            {
                return PrivacyService.Get(request.Id);
            }
            if (request.UserId != 0)
            {
                return PrivacyService.GetByUserId(request.UserId);
            }
            return new Privacy();
        }
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