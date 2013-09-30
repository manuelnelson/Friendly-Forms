using System;
using BusinessLogic.Models;
using Models;

namespace BusinessLogic.Helpers
{
    public static class ExtensionMethods
    {
       
        public static IncomeDto ToIncomeDto(this Income income)
        {
            return new IncomeDto
                {
                    HaveSalary = income.HaveSalary,
                    OtherIncome = income.OtherIncome,
                    W2Income = Convert.ToDouble(income.W2Income),
                    NonW2Income = Convert.ToDouble(income.NonW2Income),
                    SelfIncome = Convert.ToDouble(income.SelfIncome),
                    SelfIncomeNoDeductions = Convert.ToDouble(income.SelfIncomeNoDeductions),
                    Commisions = Convert.ToDouble(income.Commisions),
                    Bonuses = Convert.ToDouble(income.Bonuses),
                    Overtime = Convert.ToDouble(income.Overtime),
                    Severance = Convert.ToDouble(income.Severance),
                    Retirement = Convert.ToDouble(income.Retirement),
                    Interest = Convert.ToDouble(income.Interest),
                    Dividends = Convert.ToDouble(income.Dividends),
                    Trust = Convert.ToDouble(income.Trust),
                    Annuities = Convert.ToDouble(income.Annuities),
                    Capital = Convert.ToDouble(income.Capital),
                    SocialSecurity = Convert.ToDouble(income.SocialSecurity),
                    Compensation = Convert.ToDouble(income.Compensation),
                    Unemployment = Convert.ToDouble(income.Unemployment),
                    CivilCase = Convert.ToDouble(income.CivilCase),
                    Gifts = Convert.ToDouble(income.Gifts),
                    Prizes = Convert.ToDouble(income.Prizes),
                    Alimony = Convert.ToDouble(income.Alimony),
                    Assets = Convert.ToDouble(income.Assets),
                    Fringe = Convert.ToDouble(income.Fringe),
                    Other = Convert.ToDouble(income.Other),
                    OtherDetails = income.OtherDetails,
                };
        }
        public static ExtraExpenses ToMonthly(this ExtraExpense extraExpense)
        {
            return new ExtraExpenses
            {
                ChildId = extraExpense.ChildId,
                TutitionFather = (float)extraExpense.TutitionFather / 12,
                TutitionMother = (float)extraExpense.TutitionMother / 12,
                TutitionNonParent = (float)extraExpense.TutitionNonParent / 12,
                EducationFather = (float)extraExpense.EducationFather / 12,
                EducationMother = (float)extraExpense.EducationMother / 12,
                EducationNonParent = (float)extraExpense.EducationNonParent / 12,
                MedicalFather = (float)extraExpense.MedicalFather / 12,
                MedicalMother = (float)extraExpense.MedicalMother / 12,
                MedicalNonParent = (float)extraExpense.MedicalNonParent / 12,
                SpecialFather = (float)extraExpense.SpecialFather / 12,
                SpecialMother = (float)extraExpense.SpecialMother / 12,
                SpecialNonParent = (float)extraExpense.SpecialNonParent / 12,
                ExtraSpent = (float)extraExpense.ExtraSpent / 12,
            };

        }
    }
}