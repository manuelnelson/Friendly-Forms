using System.Collections.Generic;
using System.Linq;
using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IEnumerable<User> GetUsersClients(int userId)
        {
            //unable to parameterize - me no likey
            //http://forums.asp.net/p/1778908/4873380.aspx/1?Re+No+mapping+exists+from+object+type+f__AnonymousType3+1+System+Decimal+mscorlib+Version+4+0+0+0+Culture+neutral+PublicKeyToken+b77a5c561934e089+to+a+known+managed+provider+native+type+
            return SplitContext.Database.SqlQuery<User>(
            "Select project1.Id as [Id], project1.Email as [Email], project1.FirstName as [FirstName], project1.LastName as [LastName], project1.Password as [Password], project1.CreatedDate as [CreatedDate], " +
            "project1.FailedPasswordAttemptCount as [FailedPasswordAttemptCount], project1.FailedPasswordAttemptStart as [FailedPasswordAttemptStart], project1.Verified as [Verified], project1.RoleId as [RoleId] " +
            "from dbo.[Clients] as project2 inner join dbo.[Users] as project1 on project2.[ClientUserId] = project1.[Id] Where project2.[UserId] = " + userId);
            //new
            //    {
            //        userId
            //    });
        }

        public bool LawyerHasClient(int lawyerId, int clientId)
        {
            return GetDbSet().Any(c => c.UserId == lawyerId && c.ClientUserId == clientId);
        }
    }
}