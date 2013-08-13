using System;
using System.Collections.Generic;
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
    [Route("/Decisions/")]
    public class ReqDecisions
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public long ChildId { get; set; }
        [DataMember]
        public int Education { get; set; }
        [DataMember]
        public int HealthCare { get; set; }
        [DataMember]
        public int Religion { get; set; }
        [DataMember]
        public int ExtraCurricular { get; set; }
        [DataMember]
        public string BothResolve { get; set; }
    }

    [DataContract]
    public class RespDecisions : IHasResponseStatus
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public Decisions Decisions { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
    [Authenticate]
    public class DecisionsRestService : ServiceBase
    {
        public IDecisionsService DecisionsService { get; set; }
        public IExtraDecisionsService ExtraDecisionsService { get; set; }

        public object Get(ReqDecisions request)
        {
            return DecisionsService.GetByChildId(request.ChildId);
        }

        public object Post(ReqDecisions request)
        {
            var decisions = request.TranslateTo<Decisions>();
            decisions.UserId = Convert.ToInt32(UserSession.CustomId);

            DecisionsService.Add(decisions);
            return new RespDecisions();
        }
        public object Put(ReqDecisions request)
        {
            var decisions = request.TranslateTo<Decisions>();
            decisions.UserId = Convert.ToInt32(UserSession.CustomId);
            DecisionsService.Update(decisions);
            return new RespDecisions();
        }
    }
}