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
    [Route("/OtherChildren/")]
    public class ReqOtherChildren
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public bool IsOtherParent { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int? LegallyResponsible { get; set; }
        [DataMember]
        public int? AtHome { get; set; }
        [DataMember]
        public int? Support { get; set; }
        [DataMember]
        public int? Preexisting { get; set; }
        [DataMember]
        public int? InCourt { get; set; }
        [DataMember]
        public string Details { get; set; }

    }

    [DataContract]
    public class RespOtherChildren : IHasResponseStatus
    {
        [DataMember]
        public OtherChildren OtherChildren { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class OtherChildrenRestService : Service
    {
        public IOtherChildrenService OtherChildrenService { get; set; }

        public object Post(ReqOtherChildren request)
        {
            var otherChildrenViewModel = request.TranslateTo<OtherChildrenViewModel>();
            var otherChildren = OtherChildrenService.AddOrUpdate(otherChildrenViewModel);
            return new RespOtherChildren
                {
                    OtherChildren = otherChildren
                };
        }
    }
}