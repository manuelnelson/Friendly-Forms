﻿using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
using ServiceStack.Common.Extensions;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/Responsibility/")]
    public class ReqResponsibility
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public int BeginningVisitation { get; set; }
        [DataMember]
        public int EndVisitation { get; set; }
        [DataMember]
        public int TransportationCosts { get; set; }
        [DataMember]
        public double FatherPercentage { get; set; }
        [DataMember]
        public double MotherPercentage { get; set; }
        [DataMember]
        public string OtherDetails { get; set; }
    }

    [DataContract]
    public class RespResponsibility : IHasResponseStatus
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class ResponsibilityRestService : Service
    {
        public IResponsibilityService ResponsibilityService { get; set; }

        public object Post(ReqResponsibility request)
        {
            var responsibility = request.TranslateTo<Responsibility>();
            ResponsibilityService.Add(responsibility);
            return new RespResponsibility
                {
                    Id = responsibility.Id
                };
        }
        public object Put(ReqResponsibility request)
        {
            var responsibility = request.TranslateTo<Responsibility>();
            ResponsibilityService.Update(responsibility);
            return new RespResponsibility();
        }
    }
}