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
        public string Details { get; set; }
        public bool FatherSupervision { get; set; }
        public bool MotherSupervision { get; set; }
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
            Details = update.Details;
            NeedPrivacy = update.NeedPrivacy;
            UserId = update.UserId;
            FatherSupervision = update.FatherSupervision;
            MotherSupervision = update.MotherSupervision;
            SupervisionHow = update.SupervisionHow;
            SupervisionWhere = update.SupervisionWhere;
            SupervisionWho = update.SupervisionWho;
            SupervisionCost = update.SupervisionCost;
        }
    }
}
