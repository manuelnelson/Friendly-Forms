using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;


namespace Models
{
    public class House : IEntity, IFormEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public int MaritalHouse { get; set; }
        public string Address { get; set; }
        public string SecondaryAddress { get; set; }
        public string CityState { get; set; }
        public string ZipCode { get; set; }
        public double? RetailValue { get; set; }
        public double? MoneyOwed { get; set; }
        public double? Equity { get; set; }
        public string MortgageOwner { get; set; }
        public string Divide { get; set; }

        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<HouseViewModel>(); 
        }

        public void Update(IFormEntity entity)
        {
            var updatingEntity = (House)entity;
            UserId = updatingEntity.UserId;
            MaritalHouse = updatingEntity.MaritalHouse;
            Address = updatingEntity.Address;
            SecondaryAddress = updatingEntity.SecondaryAddress;
            CityState = updatingEntity.CityState;
            ZipCode = updatingEntity.ZipCode;
            RetailValue = updatingEntity.RetailValue;
            MoneyOwed = updatingEntity.MoneyOwed;
            Equity = updatingEntity.Equity;
            MortgageOwner = updatingEntity.MortgageOwner;
            Divide = updatingEntity.Divide;
        }
    }
}
