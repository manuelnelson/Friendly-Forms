using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
using ServiceStack.Common.Extensions;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/Deviations/")]
    public class ReqDeviations
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int Circumstances { get; set; }
        [DataMember]
        public string Unjust { get; set; }
        [DataMember]
        public string BestInterest { get; set; }
        [DataMember]
        public string Impair { get; set; }
        [DataMember]
        public int? HighLow { get; set; }
        [DataMember]
        public int? LowDeviation { get; set; }
        [DataMember]
        public string WhyLow { get; set; }
        [DataMember]
        public int? HighIncome { get; set; }
        [DataMember]
        public int? Health { get; set; }
        [DataMember]
        public int? Insurance { get; set; }
        [DataMember]
        public int? TaxCredit { get; set; }
        [DataMember]
        public int? TravelExpenses { get; set; }
        [DataMember]
        public int? Visitation { get; set; }
        [DataMember]
        public int? Alimony { get; set; }
        [DataMember]
        public int? Mortgage { get; set; }
        [DataMember]
        public int? Permanency { get; set; }
        [DataMember]
        public int? NonSpecific { get; set; }

    }

    [DataContract]
    public class RespDeviations : IHasResponseStatus
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class DeviationsRestService : Service
    {
        public IDeviationsService DeviationsService { get; set; }
        public object Get(ReqDeviations request)
        {
            if (request.Id != 0)
            {
                return DeviationsService.Get(request.Id);
            }
            if (request.UserId != 0)
            {
                return DeviationsService.GetByUserId(request.UserId);
            }
            return new Deviations();
        }
        public object Post(ReqDeviations request)
        {
            var specialCircumstances = request.TranslateTo<Deviations>();
            DeviationsService.Add(specialCircumstances);
            return new RespDeviations
                {
                    Id = specialCircumstances.Id
                };
        }
        public object Put(ReqDeviations request)
        {
            var specialCircumstances = request.TranslateTo<Deviations>();
            DeviationsService.Update(specialCircumstances);
            return new RespDeviations();
        }
    }
}