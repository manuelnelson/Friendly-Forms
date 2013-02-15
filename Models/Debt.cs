using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common.Extensions;

namespace Models
{
    public class Debt : IFormEntity
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int MaritalDebt { get; set; }
        public string DebtDivision { get; set; }
        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<DebtViewModel>();
        }

        public void Update(IFormEntity entity)
        {
            var updatingEntity = (Debt)entity;
            UserId = updatingEntity.UserId;
            MaritalDebt = updatingEntity.MaritalDebt;
            DebtDivision = updatingEntity.DebtDivision;
        }
    }
}
