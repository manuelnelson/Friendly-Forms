using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;

namespace Models
{
    public class Tax : IFormEntity
    {
        public long Id { get; set; }
        public int UserId { get; set; }
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
