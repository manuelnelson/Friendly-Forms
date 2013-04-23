﻿using System.Runtime.Serialization;
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
    public class DeviationsDto : IReturn<DeviationsDto>
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public bool IsOtherParent { get; set; }
        [DataMember]
        public long ChildId { get; set; }
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
        public int? AlimonyPaid { get; set; }
        [DataMember]
        public int? Mortgage { get; set; }
        [DataMember]
        public int? Permanency { get; set; }
        [DataMember]
        public int? NonSpecific { get; set; }
    }

    public class DeviationsRestService : Service
    {
        public IDeviationsService DeviationsService { get; set; }

        public object Get(DeviationsDto request)
        {
            if (request.ChildId != 0)
            {
                var deviations = DeviationsService.GetByChildId(request.ChildId, request.IsOtherParent) ?? new Deviations()
                    {
                        ChildId = request.ChildId,    
                    };
                deviations.HighLow = deviations.HighLow ?? 0;
                return deviations.TranslateTo<DeviationsDto>();
            }                
            return DeviationsService.Get(request.Id);
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