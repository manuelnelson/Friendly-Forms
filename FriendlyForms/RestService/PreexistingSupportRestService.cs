using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models.ViewModels;
using ServiceStack.Common.Extensions;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/PreexistingSupport/")]
    public class ReqPreexistingSupport
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public bool IsOtherParent { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int Support { get; set; }
        [DataMember]
        public string CourtName { get; set; }
        [DataMember]
        public string CaseNumber { get; set; }
        [DataMember]
        public string OrderDate { get; set; }
        [DataMember]
        public string Monthly { get; set; }
    }

    [DataContract]
    public class RespPreexistingSupport : IHasResponseStatus
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class PreexistingSupportRestService : Service
    {
        public IPreexistingSupportService PreexistingSupportService { get; set; }

        public object Post(ReqPreexistingSupport request)
        {
            var preexistingSupportViewModel = request.TranslateTo<PreexistingSupportViewModel>();
            PreexistingSupportService.AddOrUpdate(preexistingSupportViewModel);
            return new RespPreexistingSupport();
        }
    }
}