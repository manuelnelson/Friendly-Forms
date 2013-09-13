using System.Collections.Generic;
using System.Linq;
using System.Net;
using BusinessLogic.Contracts;
using FriendlyForms.Helpers;
using Models;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
namespace FriendlyForms.RestService
{
    public class AttorneyClientRestService
    {
        [Route("/AttorneyClients", "POST")]
        [Route("/AttorneyClients", "PUT")]
        [Route("/AttorneyClients", "GET")]
        [Route("/AttorneyClients", "DELETE")]
        [Route("/AttorneyClients/{Ids}")]
        public class AttorneyClientDto : IReturn<AttorneyClientDto>
        {
            public long Id { get; set; }
            public long[] Ids { get; set; }
            public long UserId { get; set; }
            public long ClientUserId { get; set; }
            public string CaseNumber { get; set; }
        }

        public class AttorneyClientsService : Service
        {
            public IAttorneyClientService AttorneyClientService { get; set; } //Injected by IOC
            public ICourtService CourtService { get; set; }
            public object Get(AttorneyClientDto request)
            {
                List<AttorneyClient> attorneyClients;
                if (request.Id > 0)
                {
                    AttorneyClient attorneyClient = AttorneyClientService.Get(request.Id);
                    return attorneyClient.ToAttorneyClientDto();
                }
                if (request.Ids != null && request.Ids.Length > 0)
                {
                    attorneyClients = AttorneyClientService.Get(request.Ids).ToList();
                    return attorneyClients.ToAttorneyClientDto();                    
                }
                if (request.UserId > 0)
                {
                    attorneyClients = AttorneyClientService.GetByUserId(request.UserId);
                    return attorneyClients.ToAttorneyClientDto();
                }

                throw new HttpError(HttpStatusCode.BadRequest, "Invalid arguments supplied.");
            }

            public object Post(AttorneyClientDto request)
            {
                var attorneyClientEntity = request.TranslateTo<AttorneyClient>();
                AttorneyClientService.Add(attorneyClientEntity);
                return attorneyClientEntity;
            }

            public object Put(AttorneyClientDto request)
            {
                var attorneyClientEntity = request.TranslateTo<AttorneyClient>();
                AttorneyClientService.Update(attorneyClientEntity);
                return attorneyClientEntity;
            }

            public void Delete(AttorneyClientDto request)
            {
                if (request.Ids != null && request.Ids.Length > 0)
                    AttorneyClientService.DeleteAll(request.Ids);
                AttorneyClientService.Delete(request.Id);
            }
        }
    }

}
