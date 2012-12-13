using System.ComponentModel.DataAnnotations;
using Models.Contract;

namespace Models.ViewModels
{
    public class DebtViewModel : IViewModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Marital Debt")]
        public int MaritalDebt { get; set; }
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        [Display(Name = "Debt Division")]
        public string DebtDivision { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return new Debt()
                {
                    DebtDivision = DebtDivision,
                    MaritalDebt = MaritalDebt,
                    UserId = UserId
                };
        }
    }
}
