using System.ComponentModel.DataAnnotations;
using Models.Contract;
using ServiceStack.Common;
using ServiceStack.Common.Extensions;

namespace Models.ViewModels
{
    public class HealthInsuranceViewModel : IViewModel
    {
        public long Id { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public int Health { get; set; }
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        [Display(Name = "Details")]
        public string HealthDescription { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return this.TranslateTo<HealthInsurance>();
        }
    }
}
