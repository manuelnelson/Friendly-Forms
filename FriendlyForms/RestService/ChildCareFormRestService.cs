using System;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using FriendlyForms.Helpers;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;

namespace FriendlyForms.RestService
{
    public class ChildCareFormRestService
    {
        [Route("/ChildCareForms", "POST")]
        [Route("/ChildCareForms", "PUT")]
        [Route("/ChildCareForms")]
        public class ChildCareFormDto : IReturn<ChildCareFormDto>, IHasUser
        {
            [DataMember]
            public long Id { get; set; }
            [DataMember]
            public long[] Ids { get; set; }
            [DataMember]
            public long UserId { get; set; }
            [DataMember]
            public int ChildrenInvolved { get; set; }
        }
        [CanViewClientInfo]
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
                ChildCareFormService.Add(childCareFormEntity);
                return childCareFormEntity;
            }

            public object Put(ChildCareFormDto request)
            {
                var childCareFormEntity = request.TranslateTo<ChildCareForm>();
                ChildCareFormService.Update(childCareFormEntity);
                return childCareFormEntity;
            }

            public void Delete(ChildCareFormDto request)
            {
                var ChildCareFormEntity = request.TranslateTo<ChildCareForm>();
                ChildCareFormService.Delete(ChildCareFormEntity);
            }
        }

    }

}
