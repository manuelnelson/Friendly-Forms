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
    [Route("/Decisions/")]
    public class ReqDecisions
    {
        [DataMember]
        public int Id { get; set; }
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
    }

    [DataContract]
    public class RespDecisions : IHasResponseStatus
    {
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
            var decisionsViewModel = request.TranslateTo<DecisionsViewModel>();
            DecisionsService.AddOrUpdate(decisionsViewModel);
            return new RespDecisions();
        }
    }
}