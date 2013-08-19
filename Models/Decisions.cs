using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;


namespace Models
{
    public class Decisions : IChildFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        
        public int Education { get; set; }
        public int HealthCare { get; set; }
        public int Religion { get; set; }
        public int ExtraCurricular { get; set; }
        public string BothResolve { get; set; }

        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public long ChildId { get; set; }
        [Ignore]
        public virtual Child Child { get; set; }
        
        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<DecisionsViewModel>();
        }

        public bool IsValid()
        {
            return UserId > 0;
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
