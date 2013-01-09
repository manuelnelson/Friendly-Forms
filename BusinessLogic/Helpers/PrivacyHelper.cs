using Models;
using Models.ViewModels;

namespace BusinessLogic.Helpers
{
    public static class PrivacyHelper
    {
        public static PrivacyViewModel ConvertToModel(this Privacy privacy)
        {
            return new PrivacyViewModel()
                {
                    NeedPrivacy = privacy.NeedPrivacy,
                    UserId = privacy.UserId
                };
        }
    }
}
