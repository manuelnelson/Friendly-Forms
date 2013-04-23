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
    [Route("/PreexistingSupport/")]
    public class ReqPreexistingSupport
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public bool IsOtherParent { get; set; }
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public int Support { get; set; }
        [DataMember]
        public string CourtName { get; set; }
        [DataMember]
        public string CaseNumber { get; set; }
        [DataMember]
        public string OrderDate { get; set; }
        [DataMember]
        public string Monthly { get; set; }
    }

    [DataContract]
    public class RespPreexistingSupport : IHasResponseStatus
    {
        [DataMember]
        public ReqPreexistingSupport PreexistingSupport { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class PreexistingSupportRestService : Service
    {
        public IPreexistingSupportService PreexistingSupportService { get; set; }

        public object Post(ReqPreexistingSupport request)
        {
            var preexistingSupportViewModel = request.TranslateTo<PreexistingSupportViewModel>();
            var entity = PreexistingSupportService.AddOrUpdate(preexistingSupportViewModel);
            var preexistSupport = entity.TranslateTo<ReqPreexistingSupport>();
            preexistSupport.OrderDate = entity.OrderDate.ToShortDateString();
            return new RespPreexistingSupport()
                {
                    PreexistingSupport = preexistSupport
                };
        }
        public object Put(ReqPreexistingSupport request)
        {
            var preexistingSupport = request.TranslateTo<PreexistingSupport>();
            PreexistingSupportService.Update(preexistingSupport);
            return new RespPreexistingSupport();
        }
    }
}