using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
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
    [Route("/Court/")]
    public class ReqCourt
    {
        [DataMember]
        public long Id { get; set; }
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
        public long Id { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class CourtRestService : Service
    {
        public ICourtService CourtService { get; set; }
        public object Get(ReqCourt request)
        {
            if (request.Id != 0)
            {
                return CourtService.Get(request.Id);    
            }
            if (request.UserId != 0)
            {
                return CourtService.Get(request.UserId);                    
            }
            return new Court();
        }
        public object Post(ReqCourt request)
        {
            var court = request.TranslateTo<Court>();
            CourtService.Add(court);
            return new RespCourt()
                {
                    Id = court.Id
                };
        }
        public object Put(ReqCourt request)
        {
            var court = request.TranslateTo<Court>();
            CourtService.Update(court);
            return new RespCourt();
        }
    }
}