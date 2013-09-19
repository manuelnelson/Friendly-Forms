using System.Collections.Generic;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using FriendlyForms.Helpers;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/ExtraDecisions/",Verbs = "POST")]
    [Route("/ExtraDecisions/")]
    public class ReqExtraDecisions : IHasUser
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public long ChildId { get; set; }
        [DataMember]
        public int DecisionMaker { get; set; }
        [DataMember]
        public string Description { get; set; }
    }

    [DataContract]
    public class RespExtraDecisions : IHasResponseStatus
    {
        [DataMember]
        public List<ExtraDecisions> ExtraDecisions { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    [DataContract]
    public class RespExtraDecisionsPost : IHasResponseStatus
    {
        [DataMember]
        public ExtraDecisions ExtraDecision { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
    [Authenticate]
    public class ExtraDecisionsRestService : ServiceBase
    {
        public IExtraDecisionsService ExtraDecisionsService { get; set; }

        public object Get(ReqExtraDecisions request)
        {            
            var extraDecisions = ExtraDecisionsService.GetByChildId(request.ChildId);
            return new RespExtraDecisions
                {
                    ExtraDecisions = extraDecisions
                };
        }

        public object Post(ReqExtraDecisions request)
        {
            var extraDecisions = request.TranslateTo<ExtraDecisions>();
            ExtraDecisionsService.Add(extraDecisions);
            return extraDecisions;
        }
        public object Put(ReqExtraDecisions request)
        {
            var extraDecisions = request.TranslateTo<ExtraDecisions>();
            ExtraDecisionsService.Update(extraDecisions);
            return new RespExtraDecisionsPost();
        }
    }
}