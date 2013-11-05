using System;
using System.Collections.Generic;
using System.Linq;
using DataInterface;
using Models;
using ServiceStack.ServiceInterface.Auth;

namespace DataLayerContext.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public User GetByUserAuthId(int userAuthId)
        {
            return GetDbSet().FirstOrDefault(u => u.UserAuthId == userAuthId);
        }

        public List<UserAuth> GetAttorneysClients(long id)
        {
            throw new NotImplementedException();//return GetDbSet().Where(x=>x.)
        }
    }
}
