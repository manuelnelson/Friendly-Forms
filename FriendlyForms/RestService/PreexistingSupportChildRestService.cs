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
    [Route("/PreexistingSupportChild/")]
    public class ReqPreexistingSupportChild
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string DateOfBirth { get; set; }
        [DataMember]
        public int Gender { get; set; }
        [DataMember]
        public int PreexistingSupportId { get; set; }
    }

    [DataContract]
    public class RespPreexistingSupportChild : IHasResponseStatus
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class PreexistingSupportChildRestService : Service
    {
        public IPreexistingSupportChildService PreexistingSupportChildService { get; set; }

        public object Post(ReqPreexistingSupportChild request)
        {
            var preexistingSupportChildViewModel = request.TranslateTo<PreexistingSupportChildViewModel>();
            PreexistingSupportChildService.AddOrUpdate(preexistingSupportChildViewModel);
            return new RespPreexistingSupportChild();
        }
    }
}