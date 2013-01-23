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
    [Route("/OtherChild/")]
    public class ReqOtherChild
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string DateOfBirth { get; set; }
        [DataMember]
        public int OtherChildrenId { get; set; }
    }

    [DataContract]
    public class RespOtherChild : IHasResponseStatus
    {
        [DataMember]
        public ReqOtherChild OtherChild { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class OtherChildRestService : Service
    {
        public IOtherChildService OtherChildService { get; set; }

        public object Post(ReqOtherChild request)
        {
            var otherChildViewModel = request.TranslateTo<OtherChildViewModel>();
            var otherChildEntity = OtherChildService.AddOrUpdate(otherChildViewModel);
            var otherChild = otherChildEntity.TranslateTo<ReqOtherChild>();
            if (otherChildEntity.DateOfBirth != null)
                otherChild.DateOfBirth = otherChildEntity.DateOfBirth.Value.ToShortDateString();
            return new RespOtherChild()
                {
                    OtherChild = otherChild
                };
        }
    }
}