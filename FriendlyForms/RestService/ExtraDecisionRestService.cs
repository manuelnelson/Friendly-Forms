using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web.Mvc;
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
    [Route("/ExtraDecisions/",Verbs = "POST")]
    [Route("/ExtraDecisions/{ChildId}", Verbs = "GET")]
    public class ReqExtraDecisions
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int ChildId { get; set; }
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

    public class ExtraDecisionsRestService : Service
    {
        public IExtraDecisionsService ExtraDecisionsService { get; set; }

        public object Get(ReqExtraDecisions request)
        {            
            var extraDecisions = ExtraDecisionsService.GetByChildId(request.ChildId);
            return extraDecisions;
        }

        public object Post(ReqExtraDecisions request)
        {
            var extraDecisionsViewModel = request.TranslateTo<ExtraDecisionsViewModel>();
            var updatedDecision = ExtraDecisionsService.AddOrUpdate(extraDecisionsViewModel);
            return new RespExtraDecisionsPost()
                {
                    ExtraDecision = updatedDecision
                };
        }
        public object Put(ReqExtraDecisions request)
        {
            var extraDecisions = request.TranslateTo<ExtraDecisions>();
            //ExtraDecisionsService.Update(extraDecisions);
            return new RespExtraDecisionsPost();
        }
    }
}