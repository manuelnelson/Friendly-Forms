using System.Collections.Generic;
using System.Linq;
using System.Net;
using BusinessLogic.Contracts;
using BusinessLogic.Properties;
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
        //Retrieves Clients for an Attorney
        [Route("/AttorneyClients/clients", "POST")]
        [Route("/AttorneyClients/clients", "PUT")]
        [Route("/AttorneyClients/clients", "GET")]
        [Route("/AttorneyClients/clients", "DELETE")]
        [Route("/AttorneyClients/clients/{Ids}")]
        public class ClientDto : IReturn<ClientDto>
        {
            public long Id { get; set; }
            public long[] Ids { get; set; }
            public long UserId { get; set; }
            public long ClientUserId { get; set; }
            public string CaseNumber { get; set; }
            public bool ChangeNotification { get; set; }
            public bool PrintNotification { get; set; }
        }
        //Retrieves Attorneys (or their underlings) who have access to a Client
        [Route("/AttorneyClients/attorneys", "POST")]
        [Route("/AttorneyClients/attorneys", "PUT")]
        [Route("/AttorneyClients/attorneys", "GET")]
        [Route("/AttorneyClients/attorneys", "DELETE")]
        [Route("/AttorneyClients/attorneys/{Ids}")]
        public class AttorneyDto : IReturn<AttorneyDto>
        {
            public long Id { get; set; }
            public long[] Ids { get; set; }
            public long UserId { get; set; }
            public long ClientUserId { get; set; }
            public string AttorneyName { get; set; }
            public bool ChangeNotification { get; set; }
            public bool PrintNotification { get; set; }
        }
        [Authenticate]
        [RequiredRole("Lawyer")]
        public class AttorneyClientsService : Service
        {
            public IAttorneyClientService AttorneyClientService { get; set; } //Injected by IOC
            public ICourtService CourtService { get; set; }
            
            public object Get(ClientDto request)
            {
                List<AttorneyClient> attorneyClients;
                if (request.Id > 0)
                {
                    AttorneyClient attorneyClient = AttorneyClientService.Get(request.Id);
                    return attorneyClient.ToClientDto();
                }
                if (request.Ids != null && request.Ids.Length > 0)
                {
                    attorneyClients = AttorneyClientService.Get(request.Ids).ToList();
                    return attorneyClients.ToClientDto();                    
                }
                if (request.UserId > 0)
                {
                    attorneyClients = AttorneyClientService.GetByUserId(request.UserId);
                    return attorneyClients.ToClientDto();
                }
                throw new HttpError(HttpStatusCode.BadRequest, "Invalid arguments supplied.");
            }

            public object Get(AttorneyDto request)
            {
                List<AttorneyClient> attorneyClients;
                if (request.Id > 0)
                {
                    AttorneyClient attorneyClient = AttorneyClientService.Get(request.Id);
                    return attorneyClient.ToAttorneyDto();
                }
                if (request.Ids != null && request.Ids.Length > 0)
                {
                    attorneyClients = AttorneyClientService.Get(request.Ids).ToList();
                    return attorneyClients.ToAttorneyDto();
                }
                if (request.ClientUserId > 0)
                {
                    attorneyClients = AttorneyClientService.GetFiltered(x => x.ClientUserId == request.ClientUserId).ToList();
                    return attorneyClients.ToAttorneyDto();
                }
                throw new HttpError(HttpStatusCode.BadRequest, "Invalid arguments supplied.");
            }
            
            public object Post(ClientDto request)
            {
                var attorneyClientEntity = request.TranslateTo<AttorneyClient>();
                AttorneyClientService.Add(attorneyClientEntity);
                return attorneyClientEntity.ToClientDto();
            }
            
            public object Put(ClientDto request)
            {
                var attorneyClientEntity = request.TranslateTo<AttorneyClient>();
                AttorneyClientService.Update(attorneyClientEntity);
                return attorneyClientEntity;
            }
            
            public void Delete(ClientDto request)
            {
                if (request.Ids != null && request.Ids.Length > 0)
                    AttorneyClientService.DeleteAll(request.Ids);
                AttorneyClientService.Delete(request.Id);
            }
        }
    }

}
