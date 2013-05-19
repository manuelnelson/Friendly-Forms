using System.Collections.Generic;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
using Models.ViewModels;
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
        public int UserId { get; set; }
        [DataMember]
        public int ChildId { get; set; }
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

    public class DecisionsRestService : Service
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
            DecisionsService.AddOrUpdate(decisions);
            return new RespDecisions();
        }
        public object Put(ReqDecisions request)
        {
            var decisions = request.TranslateTo<Decisions>();
            DecisionsService.Update(decisions);
            return new RespDecisions();
        }
    }
}