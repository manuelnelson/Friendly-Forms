﻿using System;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using FriendlyForms.Helpers;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/VehicleForm/")]
    public class ReqVehicleForm : IHasUser
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public int VehiclesInvolved { get; set; }
        [DataMember]
        public long UserId { get; set; }
    }

    [DataContract]
    public class RespVehicleForm : IHasResponseStatus
    {
        [DataMember]
        public VehicleForm VehicleForm { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
    [CanViewClientInfo]
    public class VehicleFormRestService : ServiceBase
    {
        public IVehicleFormService VehicleFormService { get; set; }
        public object Get(ReqVehicleForm request)
        {
            if (request.Id != 0)
            {
                return VehicleFormService.Get(request.Id);
            }
            return VehicleFormService.GetByUserId(request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId));
        }
        public object Post(ReqVehicleForm request)
        {
            var vehicleForm = request.TranslateTo<VehicleForm>();
            VehicleFormService.Add(vehicleForm);
            return new RespVehicleForm()
                {
                    VehicleForm = vehicleForm
                };
        }
        public object Put(ReqVehicleForm request)
        {
            var vehicleForm = request.TranslateTo<VehicleForm>();
            VehicleFormService.Update(vehicleForm);
            return new RespVehicleForm();
        }    
    }
}