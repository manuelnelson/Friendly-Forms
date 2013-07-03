using System;
using System.Collections.Generic;
using BusinessLogic.Contracts;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.RestService
{
    public class DeviationsFormRestService
    {
        //REST Resource DTO
        [Route("/DeviationsForm")]
        [Route("/DeviationsForm/{Ids}")]
        public class DeviationsFormListDto : IReturn<List<DeviationsFormDto>>
        {
            public long[] Ids { get; set; }

            public DeviationsFormListDto(params long[] ids)
            {
                Ids = ids;
            }
        }

        [Route("/DeviationsForm", "POST")]
        [Route("/DeviationsForm/", "PUT")]
        [Route("/DeviationsForm", "GET")]
        public class DeviationsFormDto : IReturn<DeviationsFormDto>
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
            public int? HealthMother { get; set; }
            public int? InsuranceMother { get; set; }
            public int? TaxCreditMother { get; set; }
            public int? TravelExpensesMother { get; set; }
            public int? VisitationMother { get; set; }
            public int? AlimonyPaidMother { get; set; }
            public int? MortgageMother { get; set; }
            public int? PermanencyMother { get; set; }
            public int? HighLow { get; set; }
            public int? LowDeviation { get; set; }
            public string WhyLow { get; set; }
            public int? HighIncome { get; set; }
            public int? HighDeviation { get; set; }
            public int? NonSpecific { get; set; }
            public int? SpecificDeviations { get; set; }
        }
        [Authenticate]
        public class DeviationsFormsService : ServiceBase
        {
            public IDeviationsFormService DeviationsFormService { get; set; } //Injected by IOC

            public object Get(DeviationsFormDto request)
            {
                request.UserId = Convert.ToInt32(UserSession.CustomId);
                return DeviationsFormService.GetByUserId(request.UserId);
            }

            public object Get(DeviationsFormListDto request)
            {
                //TODO Do something more interested.  Add query possibly 
                return DeviationsFormService.GetFiltered(t => t.Id != 0);
            }

            public object Post(DeviationsFormDto request)
            {
                var deviationsFormEntity = request.TranslateTo<DeviationsForm>();
                deviationsFormEntity.UserId = Convert.ToInt32(UserSession.CustomId);

                DeviationsFormService.Add(deviationsFormEntity);
                return deviationsFormEntity;
            }

            public object Put(DeviationsFormDto request)
            {
                var deviationsFormEntity = request.TranslateTo<DeviationsForm>();
                deviationsFormEntity.UserId = Convert.ToInt32(UserSession.CustomId);
                DeviationsFormService.Update(deviationsFormEntity);
                return deviationsFormEntity;
            }

            public void Delete(DeviationsFormListDto request)
            {
                DeviationsFormService.DeleteAll(request.Ids);
            }

            public void Delete(DeviationsFormDto request)
            {
                var deviationsFormEntity = request.TranslateTo<DeviationsForm>();
                DeviationsFormService.Delete(deviationsFormEntity);
            }
        }

    }

}
