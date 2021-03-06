﻿using System;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
using ServiceStack.Common;
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
        public long UserId { get; set; }
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
    [Authenticate]
    public class AddendumRestService : ServiceBase
    {
        public IAddendumService AddendumService { get; set; }

        public object Post(ReqAddendum request)
        {
            var addendum = request.TranslateTo<Addendum>();
            addendum.UserId = Convert.ToInt32(UserSession.CustomId);
            AddendumService.Add(addendum);
            return new RespAddendum()
                {
                    Id = addendum.Id
                };
        }
        public object Put(ReqAddendum request)
        {
            var addendum = request.TranslateTo<Addendum>();
            addendum.UserId = Convert.ToInt32(UserSession.CustomId);
            AddendumService.Update(addendum);
            return new RespAddendum();
        }
    }
}