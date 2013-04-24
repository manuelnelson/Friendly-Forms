﻿using System.Runtime.Serialization;
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

    public class OtherChildrenRestService : Service
    {
        public IOtherChildrenService OtherChildrenService { get; set; }

        public object Post(ReqOtherChildren request)
        {
            var otherChildren = request.TranslateTo<OtherChildren>();
            OtherChildrenService.Add(otherChildren);
            return otherChildren;
        }
        public void Put(ReqOtherChildren request)
        {
            var otherChildren = request.TranslateTo<OtherChildren>();
            OtherChildrenService.Update(otherChildren);            
        }
    }
}