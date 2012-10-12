using Models.ViewModels;

namespace Models
{
    public class ExtraDecisions
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChildId { get; set; }
        public int DecisionMaker { get; set; }
        public string Description { get; set; }

        public ExtraDecisionsViewModel ConvertToModel()
        {
            return new ExtraDecisionsViewModel()
                {
                    ChildId = ChildId,
                    DecisionMaker = DecisionMaker,
                    Description = Description,
                    Id = Id,
                    UserId = UserId
                };
        }
    }
}
