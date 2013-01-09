using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common.Extensions;

namespace Models
{
    public class Privacy : IFormEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
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
