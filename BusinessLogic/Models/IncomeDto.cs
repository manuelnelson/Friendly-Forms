namespace BusinessLogic.Models
{
    public class IncomeDto
    {
        public int HaveSalary { get; set; }
        public string OtherIncome { get; set; }
        public double W2Income { get; set; }
        public double NonW2Income { get; set; }
        public double SelfIncome { get; set; }
        public double SelfIncomeNoDeductions { get; set; }
        public double Commisions { get; set; }
        public double Bonuses { get; set; }
        public double Overtime { get; set; }
        public double Severance { get; set; }
        public double Retirement { get; set; }
        public double Interest { get; set; }
        public double Dividends { get; set; }
        public double Trust { get; set; }
        public double Annuities { get; set; }
        public double Capital { get; set; }
        public double SocialSecurity { get; set; }
        public double Compensation { get; set; }
        public double Unemployment { get; set; }
        public double CivilCase { get; set; }
        public double Gifts { get; set; }
        public double Prizes { get; set; }
        public double Alimony { get; set; }
        public double Assets { get; set; }
        public double Fringe { get; set; }
        public double Other { get; set; }
        public string OtherDetails { get; set; }

        //public const double MedicareTax = .0145;
        //public const double Fica = .062;
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
                    OtherDetails = OtherDetails,
                    OtherIncome = OtherIncome                    
                };
        }
    }
}