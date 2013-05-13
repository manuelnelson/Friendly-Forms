using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common.Extensions;

namespace Models
{
    public class Decisions : IEntity, IFormEntity
    {
        public long Id { get; set; }
        
        public int Education { get; set; }
        public int HealthCare { get; set; }
        public int Religion { get; set; }
        public int ExtraCurricular { get; set; }
        public string BothResolve { get; set; }

        public long UserId { get; set; }
        public virtual User User { get; set; }
        public long ChildId { get; set; }
        public virtual Child Child { get; set; }
        
        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<DecisionsViewModel>();
        }

        public void Update(IFormEntity entity)
        {
            var update = (Decisions)entity;
            ChildId = update.ChildId;
            Education = update.Education;
            ExtraCurricular = update.ExtraCurricular;
            HealthCare = update.HealthCare;
            Religion = update.Religion;
            UserId = update.UserId;
            BothResolve = update.BothResolve;
        }
    }
}
