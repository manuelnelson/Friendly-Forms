using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;


namespace Models
{
    public class ExtraDecisions : IEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public int DecisionMaker { get; set; }
        public string Description { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public long ChildId { get; set; }
        [Ignore]
        public virtual Child Child { get; set; }

        public ExtraDecisionsViewModel ConvertToModel()
        {
            return this.TranslateTo<ExtraDecisionsViewModel>();
        }
    }
}
