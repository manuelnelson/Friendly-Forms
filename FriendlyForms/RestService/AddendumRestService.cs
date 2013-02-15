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
    [Route("/Addendum/")]
    public class ReqAddendum
    {
        [DataMember]
        public long Id { get; set; }
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
        public long Id { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class AddendumRestService : Service
    {
        public IAddendumService AddendumService { get; set; }

        public object Post(ReqAddendum request)
        {
            var addendum = request.TranslateTo<Addendum>();
            AddendumService.Add(addendum);
            return new RespAddendum()
                {
                    Id = addendum.Id
                };
        }
        public object Put(ReqAddendum request)
        {
            var addendum = request.TranslateTo<Addendum>();
            AddendumService.Update(addendum);
            return new RespAddendum();
        }
    }
}