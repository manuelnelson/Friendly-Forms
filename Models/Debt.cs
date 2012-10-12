using Models.Contract;
using Models.ViewModels;

namespace Models
{
    public class Debt : IFormEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MaritalDebt { get; set; }
        public string DebtDivision { get; set; }
        public IViewModel ConvertToModel()
        {
            return new DebtViewModel()
                {
                    DebtDivision = DebtDivision,
                    MaritalDebt = MaritalDebt,
                    UserId = UserId
                };
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
