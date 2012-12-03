using System;
using System.ComponentModel.DataAnnotations;
using Models.Contract;

namespace Models.ViewModels
{
    public class IncomeViewModel : IViewModel
    {
        [Required]
        public int UserId { get; set; }

        public bool IsOtherParent { get; set; }
        [Required]
        public int Employed { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Salary { get; set; }
        [Required]
        [Display(Name = "Self Employed")]
        public int SelfEmployed { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        [Display(Name = "Income")]
        public string SelfIncome { get; set; }
        [Required]
        [Display(Name = "Tax")]
        public int SelfTax { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        [Display(Name = "Amount")]
        public string SelfTaxAmount { get; set; }
        [Required]
        [Display(Name = "Other Sources")]
        public int OtherSources { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Commisions { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Bonuses { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Overtime { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Severance { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Retirement { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Interest { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Dividends { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Trust { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Annuities { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Capital { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        [Display(Name = "Social Security")]
        public string SocialSecurity { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Compensation { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Unemployment { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        [Display(Name = "Civil Case")]
        public string CivilCase { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Gifts { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Prizes { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Alimony { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Assets { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Fringe { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Other { get; set; }
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#&_\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_\% are allowed.")]
        [Display(Name = "Other Details")]
        public string OtherDetails { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return new Income()
                {
                    UserId = UserId,
                    IsOtherParent = IsOtherParent,
                    Employed = Employed,
                    Salary = Convert.ToInt32(Salary),
                    SelfEmployed = SelfEmployed,
                    SelfIncome = Convert.ToInt32(SelfIncome),
                    SelfTax = SelfTax,
                    SelfTaxAmount = Convert.ToInt32(SelfTaxAmount),
                    OtherSources = OtherSources,
                    Commisions = Convert.ToInt32(Commisions),
                    Bonuses = Convert.ToInt32(Bonuses),
                    Overtime = Convert.ToInt32(Overtime),
                    Severance = Convert.ToInt32(Severance),
                    Retirement = Convert.ToInt32(Retirement),
                    Interest = Convert.ToInt32(Interest),
                    Dividends = Convert.ToInt32(Dividends),
                    Trust = Convert.ToInt32(Trust),
                    Annuities = Convert.ToInt32(Annuities),
                    Capital = Convert.ToInt32(Capital),
                    SocialSecurity = Convert.ToInt32(SocialSecurity),
                    Compensation = Convert.ToInt32(Compensation),
                    Unemployment = Convert.ToInt32(Unemployment),
                    CivilCase = Convert.ToInt32(CivilCase),
                    Gifts = Convert.ToInt32(Gifts),
                    Prizes = Convert.ToInt32(Prizes),
                    Alimony = Convert.ToInt32(Alimony),
                    Assets = Convert.ToInt32(Assets),
                    Fringe = Convert.ToInt32(Fringe),
                    Other = Convert.ToInt32(Other),
                    OtherDetails = OtherDetails,
                };
        }
    }
}
