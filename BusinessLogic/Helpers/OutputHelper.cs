using BusinessLogic.Models;

namespace BusinessLogic.Helpers
{
    public static class OutputHelper
    {
        public static double CalculateTotalIncome(this IncomeDto income)
        {
            return income.W2Income + income.Commisions + income.SelfIncome + income.Bonuses + income.Overtime +
                   income.Severance + income.Retirement + income.Interest + income.Dividends + income.Trust + income.Annuities +
                   income.Capital + income.SocialSecurity + income.Compensation + income.Unemployment + income.CivilCase + income.Gifts + income.Prizes +
                   income.Alimony + income.Assets + income.Fringe + income.Other;
        }
    }
}