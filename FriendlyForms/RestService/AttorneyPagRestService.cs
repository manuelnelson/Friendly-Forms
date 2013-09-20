using System.Collections.Generic;
using BusinessLogic.Contracts;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.RestService
{
    public class AttorneyPageRestService
    {
        //REST Resource DTO
        [Route("/AttorneyPages")]
        [Route("/AttorneyPages/{Ids}")]
        public class AttorneyPageListDto : IReturn<List<AttorneyPageDto>>
        {
            public long[] Ids { get; set; }

            public AttorneyPageListDto(params long[] ids)
            {
                Ids = ids;
            }
        }

        [Route("/AttorneyPages", "POST")]
        [Route("/AttorneyPages", "PUT")]
        [Route("/AttorneyPages", "GET")]
        [Route("/AttorneyPages", "DELETE")]
        public class AttorneyPageDto : IReturn<AttorneyPageDto>
        {
            public long Id { get; set; }
            public long UserId { get; set; }
            public long LawFirmId { get; set; }
            public string PageName { get; set; }
        }
        [Authenticate]
        [RequiredRole("Lawyer")]
        public class AttorneyPagesService : Service
        {
            public IAttorneyPageService AttorneyPageService { get; set; } //Injected by IOC
            public IAttorneyPageUserService AttorneyPageUserService { get; set; } //Injected by IOC
            public object Get(AttorneyPageDto request)
            {
                return AttorneyPageService.Get(request.Id);
            }

            public object Get(AttorneyPageListDto request)
            {
                //TODO Do something more interested.  Add query possibly 
                return AttorneyPageService.GetFiltered(t => t.Id != 0);
            }

            public object Post(AttorneyPageDto request)
            {
                var attorneyPageEntity = request.TranslateTo<AttorneyPage>();
                AttorneyPageService.Add(attorneyPageEntity);
                var attorneyPageUser = new AttorneyPageUser
                    {
                        AttorneyPageId = attorneyPageEntity.Id,
                        UserId = request.UserId
                    };
                AttorneyPageUserService.Add(attorneyPageUser);
                return attorneyPageEntity;
            }

            public object Put(AttorneyPageDto request)
            {
                var attorneyPageEntity = request.TranslateTo<AttorneyPage>();
                AttorneyPageService.Update(attorneyPageEntity);
                return attorneyPageEntity;
            }

            public void Delete(AttorneyPageListDto request)
            {
                AttorneyPageService.DeleteAll(request.Ids);
            }

            public void Delete(AttorneyPageDto request)
            {
                var attorneyPageEntity = request.TranslateTo<AttorneyPage>();
                AttorneyPageService.Delete(attorneyPageEntity);
            }
        }

    }

}
