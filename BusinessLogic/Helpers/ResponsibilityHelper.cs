using BusinessLogic.Models;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Helpers
{
    public static class ResponsibilityHelper
    {
        public static ResponsibilityViewModel ConvertToModel(this Responsibility responsibility)
        {
            return new ResponsibilityViewModel()
                {
                    BeginningVisitation = responsibility.BeginningVisitation,
                    EndVisitation = responsibility.EndVisitation,
                    OtherDetails = responsibility.OtherDetails,
                    FatherPercentage = responsibility.FatherPercentage,
                    MotherPercentage = responsibility.MotherPercentage,
                    TransportationCosts = responsibility.TransportationCosts,
                    UserId = responsibility.UserId
                };
        }
    }
}
