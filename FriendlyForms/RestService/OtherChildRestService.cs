using System;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
using Models.ViewModels;
using ServiceStack.Common;
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
        public long UserId { get; set; }
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
    [Authenticate]
    public class OtherChildRestService : ServiceBase
    {
        public IOtherChildService OtherChildService { get; set; }

        public object Post(ReqOtherChild request)
        {
            var otherChildViewModel = request.TranslateTo<OtherChildViewModel>();
            otherChildViewModel.UserId = Convert.ToInt64(UserSession.Id);
            var otherChildEntity = OtherChildService.AddOrUpdate(otherChildViewModel);
            var otherChild = otherChildEntity.TranslateTo<ReqOtherChild>();
            if (otherChildEntity.DateOfBirth != null)
                otherChild.DateOfBirth = otherChildEntity.DateOfBirth.Value.ToShortDateString();
            return new RespOtherChild()
                {
                    OtherChild = otherChild
                };
        }

        public object Put(ReqOtherChild request)
        {
            var otherChild = request.TranslateTo<OtherChild>();
            otherChild.UserId = Convert.ToInt64(UserSession.Id);
            OtherChildService.Update(otherChild);
            return new RespOtherChild();
        }
    }
}