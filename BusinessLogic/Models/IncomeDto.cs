namespace BusinessLogic.Models
{
    public class IncomeDto
    {
        public int HaveSalary { get; set; }
        public string OtherIncome { get; set; }
        public int W2Income { get; set; }
        public int NonW2Income { get; set; }
        public int SelfIncome { get; set; }
        public int SelfIncomeNoDeductions { get; set; }
        public int Commisions { get; set; }
        public int Bonuses { get; set; }
        public int Overtime { get; set; }
        public int Severance { get; set; }
        public int Retirement { get; set; }
        public int Interest { get; set; }
        public int Dividends { get; set; }
        public int Trust { get; set; }
        public int Annuities { get; set; }
        public int Capital { get; set; }
        public int SocialSecurity { get; set; }
        public int Compensation { get; set; }
        public int Unemployment { get; set; }
        public int CivilCase { get; set; }
        public int Gifts { get; set; }
        public int Prizes { get; set; }
        public int Alimony { get; set; }
        public int Assets { get; set; }
        public int Fringe { get; set; }
        public int Other { get; set; }
        public string OtherDetails { get; set; }

        public IncomeDto ToMonthly()
        {
            //All values are originally annual values.  Need to divide by 12 (obviously) to convert to monthly
            return new IncomeDto()
                {
                    W2Income = W2Income / 12,
                    NonW2Income = NonW2Income / 12,
                    SelfIncome = SelfIncome / 12,
                    SelfIncomeNoDeductions = SelfIncomeNoDeductions / 12,
                    Commisions = Commisions / 12,
                    Bonuses = Bonuses / 12,
                    Overtime = Overtime / 12,
                    Severance = Severance / 12,
                    Retirement = Retirement / 12,
                    Interest = Interest / 12,
                    Dividends = Dividends / 12,
                    Trust = Trust / 12,
                    Annuities = Annuities / 12,
                    Capital = Capital / 12,
                    SocialSecurity = SocialSecurity / 12,
                    Compensation = Compensation / 12,
                    Unemployment = Unemployment / 12,
                    CivilCase = CivilCase / 12,
                    Gifts = Gifts / 12,
                    Prizes = Prizes / 12,
                    Alimony = Alimony / 12,
                    Assets = Assets / 12,
                    Fringe = Fringe / 12,
                    Other = Other / 12,
                };
        }
    }
}