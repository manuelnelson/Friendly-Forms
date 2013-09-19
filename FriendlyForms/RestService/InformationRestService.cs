﻿using System;
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
    [Route("/Information/")]
    public class ReqInformation : IHasUser
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public int InformationAccess { get; set; }
        [DataMember]
        public string AccessOfRightsDetails { get; set; }
    }

    [DataContract]
    public class RespInformation : IHasResponseStatus
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
    [CanViewClientInfo]
    public class InformationRestService : ServiceBase
    {
        public IInformationService InformationService { get; set; }
        public object Get(ReqInformation request)
        {
            if (request.Id != 0)
            {
                return InformationService.Get(request.Id);
            }
            return InformationService.GetByUserId(request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId));
        }
        public object Post(ReqInformation request)
        {
            var information = request.TranslateTo<Information>();
            InformationService.Add(information);
            return new RespInformation()
                {
                    Id = information.Id
                };
        }
        public object Put(ReqInformation request)
        {
            var information = request.TranslateTo<Information>();
            InformationService.Update(information);
            return new RespInformation();
        }
    }
}