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
            public IChildCareFormService ChildCareFormService { get; set; } 

            public object Get(ChildCareFormDto request)
            {
                if (request.Id != 0)
                {
                    return ChildCareFormService.Get(request.Id);
                }
                if (request.UserId != 0)
                {
                    return ChildCareFormService.GetByUserId(request.UserId);
                }
                return new ChildCareForm();
            }

            public object Post(ChildCareFormDto request)
            {
                var childCareFormEntity = request.TranslateTo<ChildCareForm>();
                ChildCareFormService.Add(childCareFormEntity);
                return childCareFormEntity;
            }

            public object Put(ChildCareFormDto request)
            {
                var childCareFormEntity = request.TranslateTo<ChildCareForm>();
                ChildCareFormService.Update(childCareFormEntity);
                return childCareFormEntity;
            }

            public void Delete(ChildCareFormListDto request)
            {
                ChildCareFormService.DeleteAll(request.Ids);
            }

            public void Delete(ChildCareFormDto request)
            {
                var childCareFormEntity = request.TranslateTo<ChildCareForm>();
                ChildCareFormService.Delete(childCareFormEntity);
            }
        }

    }

}
