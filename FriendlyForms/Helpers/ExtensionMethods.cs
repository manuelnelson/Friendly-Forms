using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Contracts;
using FriendlyForms.RestService;
using Models;
using ServiceStack.WebHost.Endpoints;
namespace FriendlyForms.Helpers
{
    public static class ExtensionMethods
    {
        public static List<AttorneyClientRestService.AttorneyClientDto> ToAttorneyClientDto(this List<AttorneyClient> attorneyClients)
        {
            var clientIds = attorneyClients.Select(x => x.ClientUserId);
            var courtService = EndpointHost.AppHost.TryResolve<ICourtService>();
            var courts = courtService.Get(clientIds);
            var clientDtos = attorneyClients.Select(attorneyClient => new AttorneyClientRestService.AttorneyClientDto
            {
                CaseNumber = courts.First(x => x.UserId == attorneyClient.ClientUserId).CaseNumber,
                Id = attorneyClient.Id,
                ClientUserId = attorneyClient.ClientUserId,
                UserId = attorneyClient.UserId
            }).ToList();
            return clientDtos;
        }

        public static AttorneyClientRestService.AttorneyClientDto ToAttorneyClientDto(this AttorneyClient attorneyClient)
        {
            var courtService = EndpointHost.AppHost.TryResolve<ICourtService>();
            var court = courtService.Get(attorneyClient.ClientUserId);
            var attorneyClientDto = new AttorneyClientRestService.AttorneyClientDto
            {
                CaseNumber = court.CaseNumber,
                ClientUserId = attorneyClient.ClientUserId,
                Id = attorneyClient.Id,
                UserId = attorneyClient.UserId
            };
            return attorneyClientDto;
        }

    }
}