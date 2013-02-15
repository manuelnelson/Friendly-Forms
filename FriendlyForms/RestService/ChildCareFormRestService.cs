using System.Collections.Generic;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.RestService
{
    public class ChildCareFormRestService
    {
        //REST Resource DTO
        [Route("/ChildCareForm")]
        [Route("/ChildCareForm/{Ids}")]
        public class ChildCareFormListDto : IReturn<List<ChildCareFormDto>>
        {
            public long[] Ids { get; set; }

            public ChildCareFormListDto(params long[] ids)
            {
                Ids = ids;
            }
        }

        [Route("/ChildCareForm", "POST")]
        [Route("/ChildCareForm/{Id}", "PUT")]
        [Route("/ChildCareForm/{Id}", "GET")]
        public class ChildCareFormDto : IReturn<ChildCareFormDto>
        {
            [DataMember]
            public long Id { get; set; }
            [DataMember]
            public int UserId { get; set; }
            [DataMember]
            public int ChildrenInvolved { get; set; }
        }

        public class ChildCareFormsService : Service
        {
            public IChildCareFormService ChildCareFormService { get; set; } //Injected by IOC

            public object Get(ChildCareFormDto request)
            {
                return ChildCareFormService.Get(request.Id);
            }

            public object Post(ChildCareFormDto request)
            {
                var ChildCareFormEntity = request.TranslateTo<ChildCareForm>();
                ChildCareFormService.Add(ChildCareFormEntity);
                return ChildCareFormEntity;
            }

            public object Put(ChildCareFormDto request)
            {
                var ChildCareFormEntity = request.TranslateTo<ChildCareForm>();
                ChildCareFormService.Update(ChildCareFormEntity);
                return ChildCareFormEntity;
            }

            public void Delete(ChildCareFormListDto request)
            {
                ChildCareFormService.DeleteAll(request.Ids);
            }

            public void Delete(ChildCareFormDto request)
            {
                var ChildCareFormEntity = request.TranslateTo<ChildCareForm>();
                ChildCareFormService.Delete(ChildCareFormEntity);
            }
        }

    }

}
