﻿using System;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/OtherChildren/")]
    public class ReqOtherChildren : IReturn<ReqOtherChildren>
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public bool IsOtherParent { get; set; }
        [DataMember]
        public long UserId { get; set; }
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
    [Authenticate]
    public class OtherChildrenRestService : ServiceBase
    {
        public IOtherChildrenService OtherChildrenService { get; set; }

        public object Post(ReqOtherChildren request)
        {
            var otherChildren = request.TranslateTo<OtherChildren>();
            otherChildren.UserId = Convert.ToInt32(UserSession.CustomId);
            OtherChildrenService.Add(otherChildren);
            return otherChildren;
        }
        public object Put(ReqOtherChildren request)
        {
            var otherChildren = request.TranslateTo<OtherChildren>();
            otherChildren.UserId = Convert.ToInt32(UserSession.CustomId);
            OtherChildrenService.Update(otherChildren);
            return otherChildren;
        }
    }
}