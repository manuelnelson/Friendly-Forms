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
    [Route("/Responsibility/")]
    public class ReqResponsibility
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long UserId { get; set; }
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
        public long Id { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
    [Authenticate]
    public class ResponsibilityRestService : ServiceBase
    {
        public IResponsibilityService ResponsibilityService { get; set; }
        public object Get(ReqResponsibility request)
        {
            if (request.Id != 0)
            {
                return ResponsibilityService.Get(request.Id);
            }
            return ResponsibilityService.GetByUserId(request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId));
        }
        public object Post(ReqResponsibility request)
        {
            var responsibility = request.TranslateTo<Responsibility>();
            responsibility.UserId = Convert.ToInt32(UserSession.CustomId);
            ResponsibilityService.Add(responsibility);
            return new RespResponsibility
                {
                    Id = responsibility.Id
                };
        }
        public object Put(ReqResponsibility request)
        {
            var responsibility = request.TranslateTo<Responsibility>();
            responsibility.UserId = Convert.ToInt32(UserSession.CustomId);
            ResponsibilityService.Update(responsibility);
            return new RespResponsibility();
        }
    }
}