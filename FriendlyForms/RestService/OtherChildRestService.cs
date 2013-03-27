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
        public object Get(ReqOtherChild request)
        {
            if (request.Id != 0)
            {
                return OtherChildService.Get(request.Id);
            }
            if (request.UserId != 0)
            {
                return OtherChildService.GetByUserId(request.UserId);
            }
            return new OtherChild();
        }
        public object Post(ReqOtherChild request)
        {
            var otherChild = request.TranslateTo<OtherChild>();
            OtherChildService.Add(otherChild);
            request.Id = otherChild.Id;
            return new RespOtherChild()
                {
                    OtherChild = request
                };
        }

        public object Put(ReqOtherChild request)
        {
            var otherChild = request.TranslateTo<OtherChild>();
            OtherChildService.Update(otherChild);
            return new RespOtherChild();
        }
    }
}