using System;
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
    [Route("/VehicleForm/")]
    public class ReqVehicleForm
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
    [Authenticate]
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
            vehicleForm.UserId = Convert.ToInt32(UserSession.CustomId);
            VehicleFormService.Add(vehicleForm);
            return new RespVehicleForm()
                {
                    VehicleForm = vehicleForm
                };
        }
        public object Put(ReqVehicleForm request)
        {
            var vehicleForm = request.TranslateTo<VehicleForm>();
            vehicleForm.UserId = Convert.ToInt32(UserSession.CustomId);
            VehicleFormService.Update(vehicleForm);
            return new RespVehicleForm();
        }    
    }
}