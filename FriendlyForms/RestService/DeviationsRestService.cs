using System;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using FriendlyForms.Helpers;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.RestService
{

    [DataContract]
    [Route("/Deviations/")]
    public class DeviationsDto : IReturn<DeviationsDto>, IHasUser
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public int Deviation { get; set; }
        [DataMember]
        public string Unjust { get; set; }
        [DataMember]
        public string BestInterest { get; set; }
        [DataMember]
        public string Impair { get; set; }
        [DataMember]
        public int? HealthFather { get; set; }
        [DataMember]
        public int? InsuranceFather { get; set; }
        [DataMember]
        public int? TaxCreditFather { get; set; }
        [DataMember]
        public int? TravelExpensesFather { get; set; }
        [DataMember]
        public int? VisitationFather { get; set; }
        [DataMember]
        public int? AlimonyPaidFather { get; set; }
        [DataMember]
        public int? MortgageFather { get; set; }
        [DataMember]
        public int? PermanencyFather { get; set; }
        [DataMember]
        public int? NonSpecificFather { get; set; }
        [DataMember]
        public int? HealthMother { get; set; }
        [DataMember]
        public int? InsuranceMother { get; set; }
        [DataMember]
        public int? TaxCreditMother { get; set; }
        [DataMember]
        public int? TravelExpensesMother { get; set; }
        [DataMember]
        public int? VisitationMother { get; set; }
        [DataMember]
        public int? AlimonyPaidMother { get; set; }
        [DataMember]
        public int? MortgageMother { get; set; }
        [DataMember]
        public int? PermanencyMother { get; set; }
        [DataMember]
        public int? NonSpecificMother { get; set; }
        [DataMember]
        public int? HighLow { get; set; }
        [DataMember]
        public int? LowDeviation { get; set; }
        [DataMember]
        public string WhyLow { get; set; }
        [DataMember]
        public int? HighIncome { get; set; }
        [DataMember]
        public int? HighDeviation { get; set; }
        [DataMember]
        public int? SpecificDeviations { get; set; }
    }
    [CanViewClientInfo]
    public class DeviationsRestService : ServiceBase
    {
        public IDeviationsService DeviationsService { get; set; }

        public object Get(DeviationsDto request)
        {
            if (request.Id != 0)
            {
                return DeviationsService.Get(request.Id);
            }
            if (request.UserId != 0)
            {
                return DeviationsService.GetByUserId(request.UserId);
            }
            return DeviationsService.GetByUserId(Convert.ToInt32(UserSession.CustomId));
        }

        public object Post(DeviationsDto request)
        {
            var deviationsEntity = request.TranslateTo<Deviations>();
            DeviationsService.Add(deviationsEntity);
            return deviationsEntity;
        }

        public object Put(DeviationsDto request)
        {
            var deviationsEntity = request.TranslateTo<Deviations>();
            DeviationsService.Update(deviationsEntity);
            return deviationsEntity;
        }

        public void Delete(DeviationsDto request)
        {
            var deviationsEntity = request.TranslateTo<Deviations>();
            DeviationsService.Delete(deviationsEntity);
        }
    }
}