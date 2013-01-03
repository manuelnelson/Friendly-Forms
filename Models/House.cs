using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common.Extensions;

namespace Models
{
    public class House : IFormEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MaritalHouse { get; set; }
        public string Address { get; set; }
        public string CityState { get; set; }
        public string ZipCode { get; set; }
        public int? RetailValue { get; set; }
        public int? MoneyOwed { get; set; }
        public int? Equity { get; set; }
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
