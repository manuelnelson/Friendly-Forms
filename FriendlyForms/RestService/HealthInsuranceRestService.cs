using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using BusinessLogic.Contracts;
using Models.ViewModels;
using ServiceStack.Common.Extensions;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/HealthInsurance/")]
    public class ReqHealthInsurance
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int Health { get; set; }
        [DataMember]
        public string HealthDescription { get; set; }
    }

    [DataContract]
    public class RespHealthInsurance : IHasResponseStatus
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class HealthInsuranceRestService : Service
    {
        public IHealthInsuranceService HealthInsuranceService { get; set; }

        public object Post(ReqHealthInsurance request)
        {
            var healthInsuranceViewModel = request.TranslateTo<HealthInsuranceViewModel>();
            HealthInsuranceService.AddOrUpdate(healthInsuranceViewModel);
            return new RespHealthInsurance();
        }
    }
}