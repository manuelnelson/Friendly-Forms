using Models.Contract;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("Debts")]
    public class Debt : IEntity, IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public int MaritalDebt { get; set; }
        public string DebtDivision { get; set; }

        public bool IsValid()
        {
            return UserId > 0;
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
