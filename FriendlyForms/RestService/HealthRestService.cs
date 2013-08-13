using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.RestService
{
    public class HealthRestService
    {
        //REST Resource DTO
        [Route("/Healths")]
        [Route("/Healths/{Ids}")]
        public class HealthListDto : IReturn<List<HealthDto>>
        {
            public long[] Ids { get; set; }

            public HealthListDto(params long[] ids)
            {
                Ids = ids;
            }
        }

        [Route("/Healths", "POST")]
        [Route("/Healths", "PUT")]
        [Route("/Healths")]
        public class HealthDto : IReturn<HealthDto>
        {
            [DataMember]
            public long Id { get; set; }
            [DataMember]
            public long UserId { get; set; }
            [DataMember]
            public bool IsOtherParent { get; set; }
            [DataMember]
            public int ProvideHealth { get; set; }
            [DataMember]
            public bool Prorate { get; set; }
            [DataMember]
            public bool FathersHealth { get; set; }
            [DataMember]
            public bool MothersHealth { get; set; }
            [DataMember]
            public bool NonCustodialHealth { get; set; }
            [DataMember]
            public int? FathersHealthAmount { get; set; }
            [DataMember]
            public int? MothersHealthAmount { get; set; }
            [DataMember]
            public int? NonCustodialHealthAmount { get; set; }
            [DataMember]
            public int? FathersHealthPercentage { get; set; }
            [DataMember]
            public int? MothersHealthPercentage { get; set; }
            [DataMember]
            public int? NonCustodialHealthPercentage { get; set; }
        }
        [Authenticate]
        public class HealthsService : ServiceBase
        {
            public IHealthService HealthService { get; set; } //Injected by IOC

            public object Get(HealthDto request)
            {
                if (request.Id != 0)
                {
                    return HealthService.Get(request.Id);
                }
                return HealthService.GetByUserId(request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId));
            }

            public object Post(HealthDto request)
            {
                var healthEntity = request.TranslateTo<Health>();
                healthEntity.UserId = Convert.ToInt32(UserSession.CustomId);
                HealthService.Add(healthEntity);
                return new HealthDto
                    {
                        Id = healthEntity.Id
                    };
            }

            public void Put(HealthDto request)
            {
                var healthEntity = request.TranslateTo<Health>();
                healthEntity.UserId = Convert.ToInt32(UserSession.CustomId);
                HealthService.Update(healthEntity);                
            }

            public void Delete(HealthListDto request)
            {
                HealthService.DeleteAll(request.Ids);
            }

            public void Delete(HealthDto request)
            {
                var HealthEntity = request.TranslateTo<Health>();
                HealthService.Delete(HealthEntity);
            }
        }

    }

}
