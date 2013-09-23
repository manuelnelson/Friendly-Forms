using System;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using FriendlyForms.Helpers;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/OtherChildren/")]
    public class ReqOtherChildren : IReturn<ReqOtherChildren>, IHasUser
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public bool IsOtherParent { get; set; }
        [DataMember]
        public long UserId { get; set; }
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
    [CanViewClientInfo]
    public class OtherChildrenRestService : ServiceBase
    {
        public IOtherChildrenService OtherChildrenService { get; set; }
        public object Get(ReqOtherChildren request)
        {
            if (request.Id != 0)
            {
                return OtherChildrenService.Get(request.Id);
            }
            return OtherChildrenService.GetByUserId(request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId), request.IsOtherParent);
        }
        public object Post(ReqOtherChildren request)
        {
            var otherChildren = request.TranslateTo<OtherChildren>();
            OtherChildrenService.Add(otherChildren);
            return otherChildren;
        }
        public void Put(ReqOtherChildren request)
        {
            var otherChildren = request.TranslateTo<OtherChildren>();
            OtherChildrenService.Update(otherChildren);            
        }
    }
}