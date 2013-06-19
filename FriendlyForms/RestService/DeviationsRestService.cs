using System;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.RestService
{

    [DataContract]
    [Route("/Deviations/")]
    public class DeviationsDto : IReturn<DeviationsDto>
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public long ChildId { get; set; }
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
    }
    [Authenticate]
    public class DeviationsRestService : ServiceBase
    {
        public IDeviationsService DeviationsService { get; set; }

        public object Get(DeviationsDto request)
        {
            if (request.ChildId != 0)
            {
                var deviations = DeviationsService.GetByChildId(request.ChildId) ?? new Deviations()
                    {
                        ChildId = request.ChildId,    
                    };
                return deviations.TranslateTo<DeviationsDto>();
            }                
            return DeviationsService.Get(request.Id);
        }

        public object Post(DeviationsDto request)
        {
            var deviationsEntity = request.TranslateTo<Deviations>();
            deviationsEntity.UserId = Convert.ToInt32(UserSession.CustomId);
            DeviationsService.Add(deviationsEntity);
            return deviationsEntity;
        }

        public object Put(DeviationsDto request)
        {
            var deviationsEntity = request.TranslateTo<Deviations>();
            deviationsEntity.UserId = Convert.ToInt32(UserSession.CustomId);
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