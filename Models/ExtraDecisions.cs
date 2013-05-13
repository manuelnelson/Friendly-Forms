using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;
using ServiceStack.Common.Extensions;

namespace Models
{
    public class ExtraDecisions : IEntity
    {
        public long Id { get; set; }
        public int DecisionMaker { get; set; }
        public string Description { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public long ChildId { get; set; }
        public virtual Child Child { get; set; }

        public ExtraDecisionsViewModel ConvertToModel()
        {
            return this.TranslateTo<ExtraDecisionsViewModel>();
        }
    }
}
