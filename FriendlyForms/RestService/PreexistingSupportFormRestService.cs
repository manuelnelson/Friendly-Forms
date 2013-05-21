using System;
using System.Collections.Generic;
using BusinessLogic.Contracts;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.RestService
{
    public class PreexistingSupportFormRestService
    {
        //REST Resource DTO
        [Route("/PreexistingSupportForm")]
        [Route("/PreexistingSupportForm/{Ids}")]
        public class PreexistingSupportFormListDto : IReturn<List<PreexistingSupportFormDto>>
        {
            public long[] Ids { get; set; }

            public PreexistingSupportFormListDto(params long[] ids)
            {
                Ids = ids;
            }
        }

        [Route("/PreexistingSupportForm", "POST")]
        [Route("/PreexistingSupportForm", "PUT")]
        [Route("/PreexistingSupportForm/{Id}", "GET")]
        public class PreexistingSupportFormDto : IReturn<PreexistingSupportFormDto>
        {
            public long Id { get; set; }
            public bool IsOtherParent { get; set; }
            public long UserId { get; set; }
            public int Support { get; set; }
        }
        [Authenticate]
        public class PreexistingSupportFormsService : ServiceBase
        {
            public IPreexistingSupportFormService PreexistingSupportFormService { get; set; } //Injected by IOC

            public object Get(PreexistingSupportFormDto request)
            {
                return PreexistingSupportFormService.Get(request.Id);
            }

            public object Get(PreexistingSupportFormListDto request)
            {
                return PreexistingSupportFormService.Get(request.Ids);
            }

            public object Post(PreexistingSupportFormDto request)
            {
                var PreexistingSupportFormEntity = request.TranslateTo<PreexistingSupportForm>();
                PreexistingSupportFormEntity.UserId = Convert.ToInt32(UserSession.CustomId);
                PreexistingSupportFormService.Add(PreexistingSupportFormEntity);
                return PreexistingSupportFormEntity;
            }

            public object Put(PreexistingSupportFormDto request)
            {
                var PreexistingSupportFormEntity = request.TranslateTo<PreexistingSupportForm>();
                PreexistingSupportFormEntity.UserId = Convert.ToInt32(UserSession.CustomId);
                PreexistingSupportFormService.Update(PreexistingSupportFormEntity);
                return PreexistingSupportFormEntity;
            }

            public void Delete(PreexistingSupportFormListDto request)
            {
                PreexistingSupportFormService.DeleteAll(request.Ids);
            }

            public void Delete(PreexistingSupportFormDto request)
            {
                var PreexistingSupportFormEntity = request.TranslateTo<PreexistingSupportForm>();
                PreexistingSupportFormService.Delete(PreexistingSupportFormEntity);
            }
        }

    }

}
