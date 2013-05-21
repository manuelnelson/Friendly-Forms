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
        [Route("/ChildCareForm", "PUT")]
        [Route("/ChildCareForm/{Id}", "GET")]
        public class ChildCareFormDto : IReturn<ChildCareFormDto>
        {
            [DataMember]
            public long Id { get; set; }
            [DataMember]
            public long UserId { get; set; }
            [DataMember]
            public int ChildrenInvolved { get; set; }
        }
        [Authenticate]
        public class ChildCareFormsService : ServiceBase
        {
            public IChildCareFormService ChildCareFormService { get; set; } //Injected by IOC


            public object Get(ChildCareFormDto request)
            {
                if (request.Id != 0)
                {
                    return ChildCareFormService.Get(request.Id);
                }
                return ChildCareFormService.GetByUserId(request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId));
            }

            public object Post(ChildCareFormDto request)
            {
                var childCareFormEntity = request.TranslateTo<ChildCareForm>();
                childCareFormEntity.UserId = Convert.ToInt32(UserSession.CustomId);
                ChildCareFormService.Add(childCareFormEntity);
                return childCareFormEntity;
            }

            public object Put(ChildCareFormDto request)
            {
                var childCareFormEntity = request.TranslateTo<ChildCareForm>();
                childCareFormEntity.UserId = Convert.ToInt32(UserSession.CustomId);
                ChildCareFormService.Update(childCareFormEntity);
                return childCareFormEntity;
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
