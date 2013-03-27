using System.Runtime.Serialization;
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
        public long Id { get; set; }
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
        public object Get(ReqVehicle request)
        {
            if (request.Id != 0)
            {
                return VehicleService.Get(request.Id);
            }
            if (request.UserId != 0)
            {
                return VehicleService.GetByUserId(request.UserId);
            }
            return new Vehicle();
        }

        public object Post(ReqVehicle request)
        {
            var vehicle = request.TranslateTo<Vehicle>();
            VehicleService.Add(vehicle);
            return new RespVehicle()
                {
                    Vehicle = vehicle
                };
        }
        public object Put(ReqVehicle request)
        {
            var vehicle = request.TranslateTo<Vehicle>();
            VehicleService.Update(vehicle);
            return new RespVehicle();
        }
    }
}