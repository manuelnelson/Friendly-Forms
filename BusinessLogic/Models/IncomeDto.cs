﻿namespace BusinessLogic.Models
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
    }
}