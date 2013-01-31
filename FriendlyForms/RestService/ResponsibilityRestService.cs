using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models.ViewModels;
using ServiceStack.Common.Extensions;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/Responsibility/")]
    public class ReqResponsibility
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int BeginningVisitation { get; set; }
        [DataMember]
        public int EndVisitation { get; set; }
        [DataMember]
        public int TransportationCosts { get; set; }
        [DataMember]
        public double FatherPercentage { get; set; }
        [DataMember]
        public double MotherPercentage { get; set; }
        [DataMember]
        public string OtherDetails { get; set; }
    }

    [DataContract]
    public class RespResponsibility : IHasResponseStatus
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class ResponsibilityRestService : Service
    {
        public IResponsibilityService ResponsibilityService { get; set; }

        public object Post(ReqResponsibility request)
        {
            var responsibilityViewModel = request.TranslateTo<ResponsibilityViewModel>();
            ResponsibilityService.AddOrUpdate(responsibilityViewModel);
            return new RespResponsibility();
        }
    }
}