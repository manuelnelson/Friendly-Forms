using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("Privacies")]
    public class Privacy : IEntity, IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public int NeedPrivacy { get; set; }
        public int NeedSupervision { get; set; }
        public string SupervisionHow { get; set; }
        public string SupervisionWhere { get; set; }
        public string SupervisionWho { get; set; }
        public int? SupervisionCost { get; set; }

        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<PrivacyViewModel>();
        }

        public bool IsValid()
        {
            return UserId > 0;
        }

        public void Update(IFormEntity entity)
        {
            var update = (Privacy)entity;
            NeedPrivacy = update.NeedPrivacy;
            UserId = update.UserId;
            NeedSupervision = update.NeedSupervision;
            SupervisionHow = update.SupervisionHow;
            SupervisionWhere = update.SupervisionWhere;
            SupervisionWho = update.SupervisionWho;
            SupervisionCost = update.SupervisionCost;
        }
    }
}
