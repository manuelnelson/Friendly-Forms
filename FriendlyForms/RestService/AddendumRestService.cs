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
    [Route("/Addendum/")]
    public class ReqAddendum
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int HasAddendum { get; set; }
        [DataMember]
        public string AddendumDetails { get; set; }
    }

    [DataContract]
    public class RespAddendum : IHasResponseStatus
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class AddendumRestService : Service
    {
        public IAddendumService AddendumService { get; set; }

        public object Post(ReqAddendum request)
        {
            var addendumViewModel = request.TranslateTo<AddendumViewModel>();
            AddendumService.AddOrUpdate(addendumViewModel);
            return new RespAddendum();
        }
    }
}