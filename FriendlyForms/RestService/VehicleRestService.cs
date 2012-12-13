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
    [Route("/Vehicle/")]
    public class ReqVehicle
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string Make { get; set; }
        [DataMember]
        public string VehicleModel { get; set; }
        [DataMember]
        public string Year { get; set; }
        [DataMember]
        public string Owner { get; set; }
        [DataMember]
        public int Refinanced { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string RefinanceDate { get; set; }
        [DataMember]
        public int VehicleFormId { get; set; }
    }

    [DataContract]
    public class RespVehicle : IHasResponseStatus
    {
        [DataMember]
        public Vehicle Vehicle { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class VehicleRestService : Service
    {
        public IVehicleService VehicleService { get; set; }

        public object Post(ReqVehicle request)
        {
            var vehicleViewModel = request.TranslateTo<VehicleViewModel>();
            var updatedVehicle = VehicleService.AddOrUpdate(vehicleViewModel);
            return new RespVehicle()
                {
                    Vehicle = updatedVehicle
                };
        }
    }
}