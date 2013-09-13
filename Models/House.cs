using System;
using Models.Contract;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("Houses")]
    public class House : IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public int MaritalHouse { get; set; }
        public string Address { get; set; }
        public string SecondaryAddress { get; set; }
        public string CityState { get; set; }
        public string ZipCode { get; set; }
        public int? RetailValue { get; set; }
        public int? MoneyOwed { get; set; }
        public int? Equity { get; set; }
        public string MortgageOwner { get; set; }
        public string Divide { get; set; }


        public bool IsValid()
        {
            return UserId > 0;
        }

        public void Update(IFormEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
