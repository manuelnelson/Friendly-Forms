using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface ISocialSecurityService : IFormService<ISocialSecurityRepository,SocialSecurity>
    {
        SocialSecurity GetByUserId(long userId, bool isOtherParent = false);
    }
}
