using Models.Contract;
using Models.ViewModels;

namespace Models
{
    public class Decisions : IFormEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChildId { get; set; }
        public int Education { get; set; }
        public int HealthCare { get; set; }
        public int Religion { get; set; }
        public int ExtraCurricular { get; set; }
        public IViewModel ConvertToModel()
        {
            return new DecisionsViewModel()
            {
                ChildId = ChildId,
                Education = Education,
                ExtraCurricular = ExtraCurricular,
                HealthCare = HealthCare,
                Religion = Religion,
                UserId = UserId
            };
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
        }
    }
}
