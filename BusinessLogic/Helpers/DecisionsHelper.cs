using BusinessLogic.Models;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Helpers
{
    public static class DecisionsHelper
    {
        public static DecisionsViewModel ConvertToModel(this Decisions decisions)
        {
            return new DecisionsViewModel()
                {
                    ChildId = decisions.ChildId,
                    Education = decisions.Education,
                    ExtraCurricular = decisions.ExtraCurricular,
                    HealthCare = decisions.HealthCare,
                    Religion = decisions.Religion,
                    UserId = decisions.UserId
                };
        }
    }
}
