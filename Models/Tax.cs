using Models.Contract;
using Models.ViewModels;

namespace Models
{
    public class Tax : IFormEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Taxes { get; set; }
        public string TaxDescription { get; set; }
        
        public IViewModel ConvertToModel()
        {
            return new TaxViewModel()
                {
                    TaxDescription = TaxDescription,
                    Taxes = Taxes,
                    UserId = UserId
                };
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
