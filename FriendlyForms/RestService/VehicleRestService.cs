using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/Vehicles/")]
    [Route("/Vehicles", "POST")]
    [Route("/Vehicles/", "PUT")]
    [Route("/Vehicles", "GET")]
    [Route("/Vehicles/{Id}")]
    public class ReqVehicle
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long[] Ids { get; set; }
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public string Make { get; set; }
        [DataMember]
        public string Model { get; set; }
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
        public List<Vehicle> Vehicles { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
    [Authenticate]
    public class VehicleRestService : ServiceBase
    {
        public IVehicleService VehicleService { get; set; }
        public object Get(ReqVehicle request)
        {
            if (request.Ids != null && request.Ids.Length > 0)
                return VehicleService.Get(request.Ids);
            if (request.Id != 0)
                return VehicleService.Get(request.Id);
            if (request.UserId != 0)
            {
                return new RespVehicle
                {
                    Vehicles = VehicleService.GetByUserId(request.UserId).ToList()
                };                
            }
            throw new HttpError(HttpStatusCode.BadRequest, "Invalid arguments supplied.");
        }
        public object Post(ReqVehicle request)
        {
            var vehicle = request.TranslateTo<Vehicle>();
            vehicle.UserId = Convert.ToInt32(UserSession.CustomId);
            VehicleService.Add(vehicle);
            return new RespVehicle()
                {
                    Vehicle = vehicle
                };
        }
        public object Put(ReqVehicle request)
        {
            var vehicle = request.TranslateTo<Vehicle>();
            vehicle.UserId = Convert.ToInt32(UserSession.CustomId);
            VehicleService.Update(vehicle);
            return new RespVehicle();
        }
        
        public void Delete(ReqVehicle request)
        {
            if (request.Ids != null && request.Ids.Length > 0)
            {
                VehicleService.DeleteAll(request.Ids);
                return;
            }                
            if (request.Id > 0)
            {
                VehicleService.Delete(request.Id);
                return;
            }
            throw new HttpError(HttpStatusCode.BadRequest, "Invalid arguments supplied.");
        }
    }
}