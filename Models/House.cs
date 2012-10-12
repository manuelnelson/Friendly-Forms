using Models.Contract;
using Models.ViewModels;

namespace Models
{
    public class House : IFormEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MaritalHouse { get; set; }
        public string Address { get; set; }
        public int? RetailValue { get; set; }
        public int? MoneyOwed { get; set; }
        public int? Equity { get; set; }
        public string MortgageOwner { get; set; }

        public IViewModel ConvertToModel()
        {
            return new HouseViewModel()
                {
                    Address = Address,
                    Equity = Equity,
                    MaritalHouse = MaritalHouse,
                    MoneyOwed = MoneyOwed,
                    MortgageOwner = MortgageOwner,
                    RetailValue = RetailValue,
                    UserId = UserId
                };
        }

        public void Update(IFormEntity entity)
        {
            var updatingEntity = (House)entity;
            UserId = updatingEntity.UserId;
            MaritalHouse = updatingEntity.MaritalHouse;
            Address = updatingEntity.Address;
            RetailValue = updatingEntity.RetailValue;
            MoneyOwed = updatingEntity.MoneyOwed;
            Equity = updatingEntity.Equity;
            MortgageOwner = updatingEntity.MortgageOwner;
        }
    }
}
