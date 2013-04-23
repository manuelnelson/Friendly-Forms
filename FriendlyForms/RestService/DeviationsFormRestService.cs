using System.Collections.Generic;
using BusinessLogic.Contracts;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.RestService
{
    public class DeviationsFormRestService
    {
        //REST Resource DTO
        [Route("/DeviationsForm")]
        [Route("/DeviationsForm/{Ids}")]
        public class DeviationsFormListDto : IReturn<List<DeviationsFormDto>>
        {
            public long[] Ids { get; set; }

            public DeviationsFormListDto(params long[] ids)
            {
                Ids = ids;
            }
        }

        [Route("/DeviationsForm", "POST")]
        [Route("/DeviationsForm/", "PUT")]
        [Route("/DeviationsForm", "GET")]
        public class DeviationsFormDto : IReturn<DeviationsFormDto>
        {
            public long Id { get; set; }
            public long UserId { get; set; }
            public bool IsOtherParent { get; set; }
            public int Deviation { get; set; }
        }

        public class DeviationsFormsService : Service
        {
            public IDeviationsFormService DeviationsFormService { get; set; } //Injected by IOC

            public object Get(DeviationsFormDto request)
            {
                return DeviationsFormService.GetByUserId(request.UserId, request.IsOtherParent);
            }

            public object Get(DeviationsFormListDto request)
            {
                //TODO Do something more interested.  Add query possibly 
                return DeviationsFormService.GetFiltered(t => t.Id != 0);
            }

            public object Post(DeviationsFormDto request)
            {
                var DeviationsFormEntity = request.TranslateTo<DeviationsForm>();
                DeviationsFormService.Add(DeviationsFormEntity);
                return DeviationsFormEntity;
            }

            public object Put(DeviationsFormDto request)
            {
                var DeviationsFormEntity = request.TranslateTo<DeviationsForm>();
                DeviationsFormService.Update(DeviationsFormEntity);
                return DeviationsFormEntity;
            }

            public void Delete(DeviationsFormListDto request)
            {
                DeviationsFormService.DeleteAll(request.Ids);
            }

            public void Delete(DeviationsFormDto request)
            {
                var DeviationsFormEntity = request.TranslateTo<DeviationsForm>();
                DeviationsFormService.Delete(DeviationsFormEntity);
            }
        }

    }

}
