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
    public class UserRestService
    {
        ////REST Resource DTO
        [Route("/Users", "POST")]
        [Route("/Users/", "PUT")]
        [Route("/Users", "GET")]
        [Route("/Users/{Id}")]
        public class UserDto : IReturn<UserDto>
        {
            public long Id { get; set; }
            public long[] Ids { get; set; }
            public int UserAuthId { get; set; }
            public bool Paid { get; set; }
            public string DisplayName { get; set; }
            public long? LawFirmId { get; set; }
            public string Position { get; set; }
        }
        
        public class UsersService : Service
        {
            public IUserService UserService { get; set; } //Injected by IOC

            public object Get(UserDto request)
            {
                if(request.Ids != null && request.Ids.Length > 0)
                    return UserService.Get(request.Ids);
                if(request.Id > 0)
                    return UserService.Get(request.Id);
                if (request.LawFirmId > 0)
                {
                    var users = UserService.GetFiltered(x => x.LawFirmId == request.LawFirmId);
                    return users.Select(x => x.ToUserDto()).ToList();
                }
                //var cardSafeClient = new CardSafe.CardSafeSoapClient()

                throw new HttpError(HttpStatusCode.BadRequest, "Invalid arguments supplied.");
            }

            public object Post(UserDto request)
            {
                var userEntity = request.TranslateTo<User>();
                UserService.Add(userEntity);
                return userEntity;
            }

            public object Put(UserDto request)
            {
                var userEntity = request.TranslateTo<User>();
                UserService.Update(userEntity);
                return userEntity;
            }

            public void Delete(UserDto request)
            {
                if (request.Ids != null && request.Ids.Length > 0)
                {
                    UserService.DeleteAll(request.Ids);
                    return;
                }                    
                if (request.Id > 0)
                {
                    UserService.Delete(request.Id);
                    return;
                }                    
                throw new HttpError(HttpStatusCode.BadRequest, "Invalid arguments supplied.");
            }
        }

    }

}
