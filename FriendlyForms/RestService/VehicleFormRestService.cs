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
        public int Id { get; set; }
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

        public object Post(ReqVehicleForm request)
        {
            var vehicleFormViewModel = request.TranslateTo<VehicleFormViewModel>();
            var updatedModel = VehicleFormService.AddOrUpdate(vehicleFormViewModel);
            return new RespVehicleForm()
                {
                    VehicleForm = updatedModel
                };
        }
    }
}