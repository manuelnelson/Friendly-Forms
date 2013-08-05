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
    public class ChildCareRestService
    {
        //REST Resource DTO
        [Route("/ChildCare")]
        [Route("/ChildCare/{Ids}")]
        public class ChildCareListDto : IReturn<List<ChildCareDto>>
        {
            public long[] Ids { get; set; }

            public ChildCareListDto(params long[] ids)
            {
                Ids = ids;
            }
        }

        [Route("/ChildCare", "POST")]
        [Route("/ChildCare/", "PUT")]
        [Route("/ChildCare/", "GET")]
        public class ChildCareDto : IReturn<ChildCareDto>
        {
            [DataMember]
            public long Id { get; set; }
            [DataMember]
            public long UserId { get; set; }
            [DataMember]
            public long ChildId { get; set; }
            [DataMember]
            public int SchoolFather { get; set; }
            [DataMember]
            public int SchoolMother { get; set; }
            [DataMember]
            public int SchoolNonParent { get; set; }
            [DataMember]
            public int SummerFather { get; set; }
            [DataMember]
            public int SummerMother { get; set; }
            [DataMember]
            public int SummerNonParent { get; set; }
            [DataMember]
            public int BreaksFather { get; set; }
            [DataMember]
            public int BreaksMother { get; set; }
            [DataMember]
            public int BreaksNonParent { get; set; }
            [DataMember]
            public int OtherFather { get; set; }
            [DataMember]
            public int OtherMother { get; set; }
            [DataMember]
            public int OtherNonParent { get; set; }
        }
        [Authenticate]
        public class ChildCaresService : ServiceBase
        {
            public IChildCareService ChildCareService { get; set; } //Injected by IOC

            public object Get(ChildCareDto request)
            {
                if(request.ChildId !=0)
                    return ChildCareService.GetByChildId(request.ChildId).TranslateTo<ChildCareDto>();
                return ChildCareService.Get(request.Id);
            }

            public object Post(ChildCareDto request)
            {
                var childCareEntity = request.TranslateTo<ChildCare>();
                childCareEntity.UserId = Convert.ToInt32(UserSession.CustomId);    
                ChildCareService.Add(childCareEntity);
                return childCareEntity;
            }

            public object Put(ChildCareDto request)
            {
                var childCareEntity = request.TranslateTo<ChildCare>();
                childCareEntity.UserId = Convert.ToInt32(UserSession.CustomId); 
                ChildCareService.Update(childCareEntity); 
                return childCareEntity; 
            }

            public void Delete(ChildCareListDto request)
            {
                ChildCareService.DeleteAll(request.Ids);
            }

            public void Delete(ChildCareDto request)
            {
                var ChildCareEntity = request.TranslateTo<ChildCare>();
                ChildCareService.Delete(ChildCareEntity);
            }
        }

    }

}
