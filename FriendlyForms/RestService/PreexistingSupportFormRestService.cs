using System;
using BusinessLogic.Contracts;
using FriendlyForms.Helpers;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;

namespace FriendlyForms.RestService
{
    public class PreexistingSupportFormRestService
    {
        [Route("/PreexistingSupportForms", "POST")]
        [Route("/PreexistingSupportForms", "PUT")]
        [Route("/PreexistingSupportForms")]
        public class PreexistingSupportFormDto : IReturn<PreexistingSupportFormDto>, IHasUser
        {
            public long Id { get; set; }
            public long[] Ids { get; set; }
            public bool IsOtherParent { get; set; }
            public long UserId { get; set; }
            public int Support { get; set; }
        }
        [CanViewClientInfo]
        public class PreexistingSupportFormsService : ServiceBase
        {
            public IPreexistingSupportFormService PreexistingSupportFormService { get; set; } //Injected by IOC

            public object Get(PreexistingSupportFormDto request)
            {
                return PreexistingSupportFormService.GetByUserId(request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId), request.IsOtherParent);
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


            public void Delete(PreexistingSupportFormDto request)
            {
                var preexistingSupportFormEntity = request.TranslateTo<PreexistingSupportForm>();
                PreexistingSupportFormService.Delete(preexistingSupportFormEntity);
            }
        }

    }

}
