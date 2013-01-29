using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface ISocialSecurityService : IFormService<ISocialSecurityRepository,SocialSecurity>
    {
        SocialSecurityViewModel GetByUserId(int userId, bool isOtherParent = false);
        SocialSecurity AddOrUpdate(SocialSecurityViewModel model);
    }
}
