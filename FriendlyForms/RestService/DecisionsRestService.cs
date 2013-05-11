using System;
using System.Collections.Generic;
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
        public List<ExtraDecisions> ExtraDecisions { get; set; }
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
            var decision = DecisionsService.GetByChildId(request.ChildId);
            var extraDecisions = ExtraDecisionsService.GetByChildId(request.ChildId);
            return new RespDecisions()
                {
                    Decisions     = decision,
                    ExtraDecisions = extraDecisions
                };
        }

        public object Post(ReqDecisions request)
        {
            var decisions = request.TranslateTo<DecisionsViewModel>();
            decisions.UserId = Convert.ToInt32(UserSession.CustomId);

            DecisionsService.AddOrUpdate(decisions);
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