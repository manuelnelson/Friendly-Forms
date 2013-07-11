using System;
using System.Collections.Generic;
using BusinessLogic.Contracts;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.RestService
{
    //REST Resource DTO
    [Route("/Deviations")]
    [Route("/Deviations/{Ids}")]
    public class DeviationsListDto : IReturn<List<DeviationsDto>>
    {
        public long[] Ids { get; set; }

        public DeviationsListDto(params long[] ids)
        {
            Ids = ids;
        }
    }

    [Route("/Deviations", "POST")]
    [Route("/Deviations/", "PUT")]
    [Route("/Deviations", "GET")]
    public class DeviationsDto : IReturn<DeviationsDto>
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int Deviation { get; set; }
        public string Unjust { get; set; }
        public string BestInterest { get; set; }
        public string Impair { get; set; }
        public int? HealthFather { get; set; }
        public int? InsuranceFather { get; set; }
        public int? TaxCreditFather { get; set; }
        public int? TravelExpensesFather { get; set; }
        public int? VisitationFather { get; set; }
        public int? AlimonyPaidFather { get; set; }
        public int? MortgageFather { get; set; }
        public int? PermanencyFather { get; set; }
        public int? NonSpecificFather { get; set; }
        public int? HealthMother { get; set; }
        public int? InsuranceMother { get; set; }
        public int? TaxCreditMother { get; set; }
        public int? TravelExpensesMother { get; set; }
        public int? VisitationMother { get; set; }
        public int? AlimonyPaidMother { get; set; }
        public int? MortgageMother { get; set; }
        public int? PermanencyMother { get; set; }
        public int? NonSpecificMother { get; set; }
        public int? HighLow { get; set; }
        public int? LowDeviation { get; set; }
        public string WhyLow { get; set; }
        public int? HighIncome { get; set; }
        public int? HighDeviation { get; set; }
        public int? SpecificDeviations { get; set; }
    }

    [Authenticate]
    public class DeviationsRestService : ServiceBase
    {
        public IDeviationsService DeviationsService { get; set; } //Injected by IOC

        public object Get(DeviationsDto request)
        {
            request.UserId = Convert.ToInt32(UserSession.CustomId);
            return DeviationsService.GetByUserId(request.UserId);
        }

        public object Get(DeviationsListDto request)
        {
            //TODO Do something more interested.  Add query possibly 
            return DeviationsService.GetFiltered(t => t.Id != 0);
        }

        public object Post(DeviationsDto request)
        {
            var deviationsFormEntity = request.TranslateTo<Deviations>();
            deviationsFormEntity.UserId = Convert.ToInt32(UserSession.CustomId);

            DeviationsService.Add(deviationsFormEntity);
            return deviationsFormEntity;
        }

        public object Put(DeviationsDto request)
        {
            var deviationsFormEntity = request.TranslateTo<Deviations>();
            deviationsFormEntity.UserId = Convert.ToInt32(UserSession.CustomId);
            DeviationsService.Update(deviationsFormEntity);
            return deviationsFormEntity;
        }

        public void Delete(DeviationsListDto request)
        {
            DeviationsService.DeleteAll(request.Ids);
        }

        public void Delete(DeviationsDto request)
        {
            var deviationsFormEntity = request.TranslateTo<Deviations>();
            DeviationsService.Delete(deviationsFormEntity);
        }
    }


}
