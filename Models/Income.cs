using System;
using Models.Contract;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("Incomes")]
    public class Income : IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }

        public long UserId { get; set; }

        [Ignore]
        public virtual User User { get; set; }

        public bool IsOtherParent { get; set; }
        public int HaveSalary { get; set; }
        public string OtherIncome { get; set; }
        public int? W2Income { get; set; }
        public int? NonW2Income { get; set; }
        public int? SelfIncome { get; set; }
        public int? SelfIncomeNoDeductions { get; set; }
        public int? Commisions { get; set; }
        public int? Bonuses { get; set; }
        public int? Overtime { get; set; }
        public int? Severance { get; set; }
        public int? Retirement { get; set; }
        public int? Interest { get; set; }
        public int? Dividends { get; set; }
        public int? Trust { get; set; }
        public int? Annuities { get; set; }
        public int? Capital { get; set; }
        public int? SocialSecurity { get; set; }
        public int? Compensation { get; set; }
        public int? Unemployment { get; set; }
        public int? CivilCase { get; set; }
        public int? Gifts { get; set; }
        public int? Prizes { get; set; }
        public int? Alimony { get; set; }
        public int? Assets { get; set; }
        public int? Fringe { get; set; }
        public int? Other { get; set; }
        public string OtherDetails { get; set; }

        public bool IsValid()
        {
            return UserId > 0;
        }

        public void Update(IFormEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool HasNonW2Income()
        {
            return SelfIncome > 0 || Commisions > 0 || Bonuses > 0 ||
                Overtime > 0 || Severance  > 0 || Interest > 0 || Dividends > 0 || Trust > 0 || Annuities > 0 || Capital > 0 ||
                SocialSecurity > 0 || Compensation > 0 || Unemployment > 0 || CivilCase > 0 || Gifts > 0 || Prizes > 0 ||
                Alimony > 0 || Assets > 0 || Fringe > 0 || Other > 0;

            //SelfIncomeNoDeductions not used currently

        }
    }
}
