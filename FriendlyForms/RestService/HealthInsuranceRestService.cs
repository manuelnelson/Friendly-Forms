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
    [Route("/HealthInsurance/")]
    public class ReqHealthInsurance
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public int Health { get; set; }
        [DataMember]
        public string HealthDescription { get; set; }
    }

    [DataContract]
    public class RespHealthInsurance : IHasResponseStatus
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
    [Authenticate]
    public class HealthInsuranceRestService : ServiceBase
    {
        public IHealthInsuranceService HealthInsuranceService { get; set; }
        public object Get(ReqHealthInsurance request)
        {
            if (request.Id != 0)
            {
                return HealthInsuranceService.Get(request.Id);
            }
            return HealthInsuranceService.GetByUserId(request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId));
        }
        public object Post(ReqHealthInsurance request)
        {
            var healthInsurance = request.TranslateTo<HealthInsurance>();
            healthInsurance.UserId = Convert.ToInt32(UserSession.CustomId);
            HealthInsuranceService.Add(healthInsurance);
            return new RespHealthInsurance()
                {
                    Id = healthInsurance.Id
                };
        }
        public object Put(ReqHealthInsurance request)
        {
            var healthInsurance = request.TranslateTo<HealthInsurance>();
            healthInsurance.UserId = Convert.ToInt32(UserSession.CustomId);
            HealthInsuranceService.Update(healthInsurance);
            return new RespHealthInsurance();
        }
    }
}