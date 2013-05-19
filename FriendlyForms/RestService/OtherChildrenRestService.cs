﻿using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/OtherChildren/")]
    public class ReqOtherChildren
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public bool IsOtherParent { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int? LegallyResponsible { get; set; }
        [DataMember]
        public int? AtHome { get; set; }
        [DataMember]
        public int? Support { get; set; }
        [DataMember]
        public int? Preexisting { get; set; }
        [DataMember]
        public int? InCourt { get; set; }
        [DataMember]
        public string Details { get; set; }

    }

    [DataContract]
    public class RespOtherChildren : IHasResponseStatus
    {
        [DataMember]
        public OtherChildren OtherChildren { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class OtherChildrenRestService : Service
    {
        public IOtherChildrenService OtherChildrenService { get; set; }
        public object Get(ReqOtherChildren request)
        {
            if (request.Id != 0)
            {
                return OtherChildrenService.Get(request.Id);
            }
            if (request.UserId != 0)
            {
                return OtherChildrenService.GetByUserId(request.UserId);
            }
            return new OtherChildren();
        }
        public object Post(ReqOtherChildren request)
        {
            var otherChildrenViewModel = request.TranslateTo<OtherChildren>();
            OtherChildrenService.Add(otherChildrenViewModel);
            return new RespOtherChildren();
        }
        public object Put(ReqOtherChildren request)
        {
            var otherChildren = request.TranslateTo<OtherChildren>();
            OtherChildrenService.Update(otherChildren);
            return new RespOtherChildren();
        }
    }
}