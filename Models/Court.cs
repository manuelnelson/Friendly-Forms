using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("Courts")]
    public class Court : IEntity, IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public int CountyId { get; set; }
        public string CaseNumber { get; set; }
        public int AuthorOfPlan { get; set; }
        public int PlanType { get; set; }
        public string ExistCaseNumber { get; set; }

        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<CourtViewModel>();
        }

        public void Update(IFormEntity entity)
        {
            var update = (Court) entity;
            AuthorOfPlan = update.AuthorOfPlan;
            CaseNumber = update.CaseNumber;
            CountyId = update.CountyId;
            PlanType = update.PlanType;
            ExistCaseNumber = update.ExistCaseNumber;            
            UserId = update.UserId;
        }

   

    }
}
