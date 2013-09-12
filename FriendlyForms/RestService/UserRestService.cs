using System.Collections.Generic;
using BusinessLogic.Contracts;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.RestService
{
    public class UserRestService
    {
        //REST Resource DTO
        [Route("/Users")]
        [Route("/Users/{Ids}")]
        public class UserListDto : IReturn<List<UserDto>>
        {
            public long[] Ids { get; set; }

            public UserListDto(params long[] ids)
            {
                Ids = ids;
            }
        }

        [Route("/Users", "POST")]
        [Route("/Users/", "PUT")]
        [Route("/Users/{Id}", "GET")]
        [Route("/Users", "GET")]
        public class UserDto : IReturn<UserDto>
        {
            public long Id { get; set; }
            public int UserAuthId { get; set; }
            public bool Verified { get; set; }
            public int? LawFirmId { get; set; }
            public string Position { get; set; }
        }

        public class UsersService : Service
        {
            public IUserService UserService { get; set; } //Injected by IOC

            public object Get(UserDto request)
            {
                var user = UserService.Get(request.Id);
                return user;
            }

            public object Get(UserListDto request)
            {
                //TODO Do something more interested.  Add query possibly 
                return UserService.GetFiltered(t => t.Id != 0);
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

            public void Delete(UserListDto request)
            {
                UserService.DeleteAll(request.Ids);
            }

            public void Delete(UserDto request)
            {
                var userEntity = request.TranslateTo<User>();
                UserService.Delete(userEntity);
            }
        }

    }

}
