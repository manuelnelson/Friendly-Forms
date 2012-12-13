using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using BusinessLogic.Contracts;
using Models.ViewModels;
using ServiceStack.Common.Extensions;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/Court/","POST")]
    public class ReqCourt
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int CountyId { get; set; }
        [DataMember]
        public string CaseNumber { get; set; }
        [DataMember]
        public int AuthorOfPlan { get; set; }
        [DataMember]
        public int PlanType { get; set; }
        [DataMember]
        public string ExistCaseNumber { get; set; }
    }

    [DataContract]
    public class RespCourt : IHasResponseStatus
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class CourtRestService : Service
    {
        public ICourtService CourtService { get; set; }

        public object Post(ReqCourt request)
        {
            var courtViewModel = request.TranslateTo<CourtViewModel>();
            CourtService.AddOrUpdate(courtViewModel);
            return new RespCourt();
        }
    }
}