using Models.ViewModels;
using ServiceStack.Common.Extensions;

namespace Models
{
    public class ExtraDecisions
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int ChildId { get; set; }
        public int DecisionMaker { get; set; }
        public string Description { get; set; }

        public ExtraDecisionsViewModel ConvertToModel()
        {
            return this.TranslateTo<ExtraDecisionsViewModel>();
        }
    }
}
