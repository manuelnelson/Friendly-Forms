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
    [Route("/ChildSupports/")]
    public class ReqChildSupport
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public int SupportBegin { get; set; }
        [DataMember]
        public DateTime? BeginDate { get; set; }
        [DataMember]
        public int SupportEnd { get; set; }
        [DataMember]
        public DateTime? EndDate { get; set; }
        [DataMember]
        public int BothParties { get; set; }
    }

    [DataContract]
    public class RespChildSupport : IHasResponseStatus
    {
        [DataMember]
        public ChildSupport ChildSupport { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
    [Authenticate]
    public class ChildSupportRestService : ServiceBase
    {
        public IChildSupportService ChildSupportService { get; set; }
        public object Get(ReqChildSupport request)
        {
            if (request.Id != 0)
            {
                return ChildSupportService.Get(request.Id);
            }
            return ChildSupportService.GetByUserId(request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId));
        }
        public object Post(ReqChildSupport request)
        {
            var childSupport = request.TranslateTo<ChildSupport>();
            childSupport.UserId = Convert.ToInt32(UserSession.CustomId);
            ChildSupportService.Add(childSupport);
            return new RespChildSupport()
                {
                    ChildSupport = childSupport
                };
        }
        public object Put(ReqChildSupport request)
        {
            var childSupport = request.TranslateTo<ChildSupport>();
            childSupport.UserId = Convert.ToInt32(UserSession.CustomId);
            ChildSupportService.Update(childSupport);
            return new RespChildSupport();
        }
    }
}