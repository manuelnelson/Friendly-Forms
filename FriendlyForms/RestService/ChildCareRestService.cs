using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using FriendlyForms.Helpers;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.RestService
{
    public class ChildCareRestService
    {
        [Route("/ChildCares", "POST")]
        [Route("/ChildCares/", "PUT")]
        [Route("/ChildCares")]
        public class ChildCareDto : IReturn<ChildCareDto>, IHasUser
        {
            [DataMember]
            public long Id { get; set; }
            [DataMember]
            public long[] Ids { get; set; }
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
                ChildCareService.Add(childCareEntity);
                return childCareEntity;
            }

            public object Put(ChildCareDto request)
            {
                var childCareEntity = request.TranslateTo<ChildCare>();
                ChildCareService.Update(childCareEntity); 
                return childCareEntity; 
            }

            public void Delete(ChildCareDto request)
            {
                var ChildCareEntity = request.TranslateTo<ChildCare>();
                ChildCareService.Delete(ChildCareEntity);
            }
        }

    }

}
