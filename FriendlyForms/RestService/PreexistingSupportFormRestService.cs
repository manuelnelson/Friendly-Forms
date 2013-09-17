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
        [Route("/PreexistingSupportForms")]
        [Route("/PreexistingSupportForms/{Ids}")]
        public class PreexistingSupportFormListDto : IReturn<List<PreexistingSupportFormDto>>
        {
            public long[] Ids { get; set; }

            public PreexistingSupportFormListDto(params long[] ids)
            {
                Ids = ids;
            }
        }

        [Route("/PreexistingSupportForms", "POST")]
        [Route("/PreexistingSupportForms", "PUT")]
        [Route("/PreexistingSupportForms")]
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
                return PreexistingSupportFormService.GetByUserId(request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId), request.IsOtherParent);
            }

            public object Get(PreexistingSupportFormListDto request)
            {
                return PreexistingSupportFormService.Get(request.Ids);
            }

            public object Post(PreexistingSupportFormDto request)
            {
                var preexistingSupportFormEntity = request.TranslateTo<PreexistingSupportForm>();
                PreexistingSupportFormService.Add(preexistingSupportFormEntity);
                return preexistingSupportFormEntity;
            }

            public object Put(PreexistingSupportFormDto request)
            {
                var preexistingSupportFormEntity = request.TranslateTo<PreexistingSupportForm>();
                PreexistingSupportFormService.Update(preexistingSupportFormEntity);
                return preexistingSupportFormEntity;
            }

            public void Delete(PreexistingSupportFormListDto request)
            {
                PreexistingSupportFormService.DeleteAll(request.Ids);
            }

            public void Delete(PreexistingSupportFormDto request)
            {
                var preexistingSupportFormEntity = request.TranslateTo<PreexistingSupportForm>();
                PreexistingSupportFormService.Delete(preexistingSupportFormEntity);
            }
        }

    }

}
