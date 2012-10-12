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
                    FatherCosts = responsibility.FatherCosts,
                    FatherPercentage = responsibility.FatherPercentage,
                    MotherCosts = responsibility.MotherCosts,
                    MotherPercentage = responsibility.MotherPercentage,
                    TransportationCosts = responsibility.TransportationCosts,
                    UserId = responsibility.UserId
                };
        }
    }
}
