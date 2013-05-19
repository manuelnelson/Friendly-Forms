using System.ComponentModel.DataAnnotations;
using Models.Contract;
using ServiceStack.Common;

namespace Models.ViewModels
{
    public class IncomeViewModel : IViewModel
    {
        public long Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public bool IsOtherParent { get; set; }
        [Required]
        public int HaveSalary { get; set; }
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        [Display(Name = "Other Income")]
        public int? OtherIncome { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        [Display(Name = "W2 Income")]
        public int? W2Income { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        [Display(Name = "Non W2 Income")]
        public int? NonW2Income { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        [Display(Name = "Income")]
        public int? SelfIncome { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        [Display(Name = "Income No Deductions")]
        public int? SelfIncomeNoDeductions { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public int? Commisions { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public int? Bonuses { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public int? Overtime { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public int? Severance { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public int? Retirement { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public int? Interest { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public int? Dividends { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public int? Trust { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public int? Annuities { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public int? Capital { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        [Display(Name = "Social Security")]
        public int? SocialSecurity { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public int? Compensation { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public int? Unemployment { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        [Display(Name = "Civil Case")]
        public int? CivilCase { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public int? Gifts { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public int? Prizes { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public int? Alimony { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public int? Assets { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public int? Fringe { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public int? Other { get; set; }
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        [Display(Name = "Other Details")]
        public string OtherDetails { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return this.TranslateTo<Income>();
        }
    }
}
