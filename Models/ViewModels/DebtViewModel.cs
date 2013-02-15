using System.ComponentModel.DataAnnotations;
using Models.Contract;
using ServiceStack.Common.Extensions;

namespace Models.ViewModels
{
    public class DebtViewModel : IViewModel
    {
        public long Id { get; set; }
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
            return this.TranslateTo<Debt>();
        }
    }
}
