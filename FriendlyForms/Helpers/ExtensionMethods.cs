using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BusinessLogic.Contracts;
using FriendlyForms.RestService;
using Models;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.WebHost.Endpoints;
namespace FriendlyForms.Helpers
{
    public static class ExtensionMethods
    {
        public static List<AttorneyClientRestService.ClientDto> ToClientDto(this List<AttorneyClient> attorneyClients)
        {
            var courtService = EndpointHost.AppHost.TryResolve<ICourtService>();
            return attorneyClients.Select(attorneyClient => attorneyClient.ToClientDto(courtService)).ToList();
        }

        public static AttorneyClientRestService.ClientDto ToClientDto(this AttorneyClient attorneyClient, ICourtService courtService = null)
        {
            if (courtService == null)
                courtService = EndpointHost.AppHost.TryResolve<ICourtService>();
            var court = courtService.GetByUserId(attorneyClient.ClientUserId) as Court;
            var attorneyClientDto = new AttorneyClientRestService.ClientDto
            {
                CaseNumber = court.CaseNumber,
                ClientUserId = attorneyClient.ClientUserId,
                Id = attorneyClient.Id,
                UserId = attorneyClient.UserId,
            };
            return attorneyClientDto;
        }
        public static List<AttorneyClientRestService.AttorneyDto> ToAttorneyDto(this List<AttorneyClient> attorneyClients)
        {
            var userAuthRepository = EndpointHost.AppHost.TryResolve<IUserAuthRepository>();
            var userService = EndpointHost.AppHost.TryResolve<IUserService>();
            return attorneyClients.Select(attorneyClient => attorneyClient.ToAttorneyDto(userAuthRepository, userService)).ToList();
        }
        public static AttorneyClientRestService.AttorneyDto ToAttorneyDto(this AttorneyClient attorneyClient, IUserAuthRepository userAuthRepository = null, IUserService userService = null)
        {
            if(userAuthRepository== null)
                userAuthRepository = EndpointHost.AppHost.TryResolve<IUserAuthRepository>();
            if(userService == null)
                userService = EndpointHost.AppHost.TryResolve<IUserService>();
            var user = userService.Get(attorneyClient.UserId);
            var userAuth = userAuthRepository.GetUserAuth(user.UserAuthId.ToString(CultureInfo.InvariantCulture));
            var attorneyDto = new AttorneyClientRestService.AttorneyDto
            {
                Id = attorneyClient.Id,
                ClientUserId = attorneyClient.ClientUserId,
                UserId = attorneyClient.UserId,
                AttorneyName = userAuth.DisplayName,
                PrintNotification = attorneyClient.PrintNotification,
                ChangeNotification = attorneyClient.ChangeNotification
            };
            return attorneyDto;
        }
        public static UserRestService.UserDto ToUserDto(this User user, IUserAuthRepository userAuthRepository = null)
        {
            if (userAuthRepository == null)
                userAuthRepository = EndpointHost.AppHost.TryResolve<IUserAuthRepository>();
            var userAuth = userAuthRepository.GetUserAuth(user.UserAuthId.ToString(CultureInfo.InvariantCulture));
            var attorneyDto = new UserRestService.UserDto
            {
                Id = user.Id,
                LawFirmId = user.LawFirmId,
                DisplayName = userAuth.DisplayName,
                Verified = user.Verified,
                Position = user.Position,
                UserAuthId = user.UserAuthId
            };
            return attorneyDto;
        }

    }
}