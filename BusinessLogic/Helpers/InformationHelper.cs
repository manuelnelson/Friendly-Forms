using BusinessLogic.Models;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Helpers
{
    public static class InformationHelper
    {
        public static InformationViewModel ConvertToModel(this Information information)
        {
            return new InformationViewModel()
                {
                    InformationAccess = information.InformationAccess,
                    UserId = information.UserId
                };
        }
    }
}
