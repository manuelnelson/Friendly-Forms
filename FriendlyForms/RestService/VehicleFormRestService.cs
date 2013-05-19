using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
using Models.ViewModels;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/VehicleForm/")]
    public class ReqVehicleForm
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public int VehiclesInvolved { get; set; }
        [DataMember]
        public int UserId { get; set; }
    }

    [DataContract]
    public class RespVehicleForm : IHasResponseStatus
    {
        [DataMember]
        public VehicleForm VehicleForm { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class VehicleFormRestService : Service
    {
        public IVehicleFormService VehicleFormService { get; set; }
        public object Get(ReqVehicleForm request)
        {
            if (request.Id != 0)
            {
                return VehicleFormService.Get(request.Id);
            }
            if (request.UserId != 0)
            {
                return VehicleFormService.GetByUserId(request.UserId);
            }
            return new VehicleForm();
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