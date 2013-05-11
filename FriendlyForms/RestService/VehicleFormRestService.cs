using System;
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

        public object Post(ReqVehicleForm request)
        {
            var vehicleFormViewModel = request.TranslateTo<VehicleFormViewModel>();
            vehicleFormViewModel.UserId = Convert.ToInt32(UserSession.CustomId);
            var updatedModel = VehicleFormService.AddOrUpdate(vehicleFormViewModel);
            return new RespVehicleForm()
                {
                    VehicleForm = updatedModel
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