using Models.Contract;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("Taxes")]
    public class Tax : IEntity, IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public int Taxes { get; set; }
        public string TaxDescription { get; set; }
        
        public bool IsValid()
        {
            return UserId > 0;
        }

        public void Update(IFormEntity entity)
        {
            var updatingEntity = (Tax) entity;
            UserId = updatingEntity.UserId;
            Taxes = Taxes;
            TaxDescription = TaxDescription;
        }
    }
}
