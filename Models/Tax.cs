using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;
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
        
        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<TaxViewModel>();
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
