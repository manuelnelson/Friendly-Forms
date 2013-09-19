using System;
using System.Collections.Generic;
using System.Linq;
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
    [Route("/OtherChild/")]
    public class ReqOtherChild : IHasUser
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
        public OtherChild OtherChild { get; set; }
        [DataMember]
        public List<OtherChild> OtherChildren { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
    [Authenticate]
    public class OtherChildRestService : ServiceBase
    {
        public IOtherChildService OtherChildService { get; set; }

        public object Get(ReqOtherChild request)
        {
            if (request.Id != 0)
            {
                return OtherChildService.Get(request.Id);
            }
            return new RespOtherChild
                {
                    OtherChildren = OtherChildService.GetChildrenByOtherChildrenId(request.OtherChildrenId).ToList()
                };
        }
    
        public object Post(ReqOtherChild request)
        {
            var otherChildEntity = request.TranslateTo<OtherChild>();
            OtherChildService.Add(otherChildEntity);
            return new RespOtherChild()
                {
                    OtherChild = otherChildEntity
                };
        }

        public object Put(ReqOtherChild request)
        {
            var otherChild = request.TranslateTo<OtherChild>();
            OtherChildService.Update(otherChild);
            return new RespOtherChild();
        }
        public object Delete(ReqOtherChild request)
        {
            OtherChildService.Delete(request.Id);
            return null;
        }
    }
}